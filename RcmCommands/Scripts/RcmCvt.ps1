<#--------------------------------------------------------------
<copyright file="RcmCvt.ps1" company="Microsoft">
    Copyright (c) Microsoft Corporation. All rights reserved.
</copyright>

Description: RCM CVT test end to end.

History:     22-Dec-2016   chpadh   Created

---------------------------------------------------------------#>

$ErrorActionPreference = "Stop"

$scriptRoot = $PSScriptRoot
$scriptConfigFile = "$scriptRoot\Configuration.xml"
$configData = ""

$logPath ="$env:temp\RcmCVT"
$timeStamp = (Get-Date -Format "yyyyMd_HHmmss").Replace("/","").Replace(" ","_").Replace(":","")
$logName = "execution_$timeStamp.log" 
$logFullPath = $logPath + "\" + $logName 

Write-Host "Script root path: $scriptRoot"
Write-Host "Log path: $logFullPath"
Read-Host -Prompt "Press Enter to proceed.."

try
{
    # Import module and list commands
    Import-Module "$scriptRoot\..\RcmCommands.dll"
    Get-Command -module RcmCommands

    # Load common library methods
    . "$scriptRoot\CommonLib.ps1"

    # Crate Log File
    CreateLogFile $logPath $logFullPath

    # Import Config XML
    $file = Get-Item -Path $scriptConfigFile
    if ($file.Exists) 
    {
        [xml]$configData = Get-Content $scriptConfigFile
    }
    else
    {
        throw [System.IO.FileNotFoundException] "$scriptConfigFile not found."
    }

    # Config Data
    $commands = $configData.Configuration.Commands;
    $commands.Command | Format-Table

    # Execute commands
    foreach($node in $commands.ChildNodes)
    {
        [string] $active = $node.attributes['Active'].value
        [string] $type = $node.attributes['Type'].value
        [string] $cmd = $node.attributes['Cmd'].value
        [string] $inputFile = $node.attributes['InputFile'].value
        [string] $step = $node.attributes['Step'].value

        WriteOutput "{$step}: $cmd, $active, $type, $inputFile" Cyan
        
        #if ($active -eq "Y" -and $step -eq "RegisterMachine")
         if ($active -eq "Y")
        {
            $waitingForStepCompletion = $true;
            do
            {
                if ($inputFile -ne "")
                {
                    $input = Get-Input -SourceFilePath $inputFile
                    WriteOutput $input

                    $output = &$cmd -Input $input
                }
                else
                {
                    $parameters = GetInputParameters $cmd

                    $output = &$cmd @parameters
                }

                if ($type -eq "Async")
                {
                    $workflowId = $output.WorkflowId
                    WaitForCompletion $workflowId
                }
                elseif ($output -ne "")
                {
                    WriteOutput $output
                }

                $waitingForStepCompletion = TrackForStepCompletion $step $output
            }
            While ($waitingForStepCompletion)
        }
        else
        {
            WriteOutput "Cmdlet: $cmd execution skipped." Cyan
        }
    }

    # Register Identity
    # $registerIdentityInput = Get-RegisterIdentityInput -XmlPath F:\padhi\RCM\src\Test\RcmTests\Commands.Rcm\bin\Debug\Inputs\RegisterIdentityInput.xml  
    # New-Identity -Input $registerIdentityInput;


    # [xml] $xdoc = get-content $scriptConfigFile
    # $xdoc.Configuration.WorkflowIds.EnableProtectionToAzure = 'workflowid'
    # $xdoc.Save($scriptConfigFile) 
}
catch
{
    Write-Warning $_.Exception.Message
}
