<#--------------------------------------------------------------
<copyright file="CommonLib.ps1" company="Microsoft">
    Copyright (c) Microsoft Corporation. All rights reserved.
</copyright>

Description: Script contains common functions used in test script.

History:     22-Dec-2016   chpadh   Created

---------------------------------------------------------------#>

# Parse Configuration file
function LoadConfig([string]$filename) {

    $file = Get-Item -Path $filename
    if ($file.Exists) {
        $root = "config"
        $cfg = [xml] ( Get-Content -Path $filename )
        return $cfg.config
    }
    else {
        throw [System.IO.FileNotFoundException] "$filename not found."
    }
}

# Creates log file
function CreateLogFile([string]$filePath, [string]$fileFullPath)
{
    If((Test-Path $filePath -pathtype container) -eq $False){ 
        md $filePath
    }
    
    # Check if file exists and delete it
    If((Test-Path -Path $fileFullPath)){ 
        Remove-Item -Path $fileFullPath -Force 
    }
    
    # Create file and start logging 
    
    Add-Content -Path $fileFullPath -Value "***************************************************************************************************" 
    Add-Content -Path $fileFullPath -Value " Start time: [$([DateTime]::Now)]." 
    Add-Content -Path $fileFullPath -Value "" 
}

# Write to Output/ Host
function WriteOutput($output, $color="", $isLogToFile=$true)
{
    if($color -ne "")
    {
        if($color -eq "Error")
        {
            Write-Error $output
        }
        else
        {
            $t = $host.ui.RawUI.ForegroundColor
            $host.ui.RawUI.ForegroundColor = $color
            Write-Output $output
            $host.ui.RawUI.ForegroundColor = $t
        }
    }
    else 
    {
        Write-Output $output
    }
    
    if($isLogToFile -eq $true)
    {
        Add-Content -Path $logFullPath -Value $output
    }
}


# returns random number
function GetRandom()
{
    $rand = Get-Random -minimum 1 -maximum 1000 
    return $rand
}

# returns current script path.
function GetPSScriptRoot()
{
    $scriptRoot = ""

    Try
    {
        $scriptRoot = Get-Variable -Name PSScriptRoot -ValueOnly -ErrorAction Stop
    }
    Catch
    {
        $scriptRoot = Split-Path $script:MyInvocation.MyCommand.Path
    }

    Write-Output $scriptRoot
}

# Wait function for async calls
function WaitForCompletion([string]$workflowId)
{
    $waitTimeInSeconds = 10
    $waitingForCompletion = $true;
    do
    {
        $workflow = Get-Workflow -Name $workflowId
        if($workflow.ExecutionStatus -eq "Pending")
        {
            $waitingForCompletion = $true
        }
        else
        {
            $waitingForCompletion = $false
        }

        if($waitingForCompletion)
        {
            Write-Host "Workflow ID:" + $($workflowId)
            Write-Host "Workflow execution is in Progress..."
            Write-Host "Waiting for: " + $waitTimeInSeconds.ToString() + " Seconds"
            
            Start-Sleep -Seconds $waitTimeInSeconds
        }
        else
        {
            if($workflow.ExecutionStatus -eq "Failed")
            {
                throw "Workflow execution failed.";
            }else {
                Write-Host "Workflow execution completed." -ForegroundColor Green
            }
        }
    } While($waitingForCompletion)
}

# Returns input parameters for the CmdLet
function GetInputParameters([string]$cmd)
{
    $parameters = @{}

    switch ($cmd)
    {
        "Get-MachinesForManagementId" 
        { 
            $parameters.Add("ManagementId", $configData.Configuration.ProtectionData.ManagementId)
            break 
        }

        "Get-Machine" 
        { 
            $parameters.Add("MachineId", $configData.Configuration.ProtectionData.MachineId)
            break 
        }

        "Get-ProtectionPairRecoveryPoints" 
        { 
            $parameters.Add("ProtectionPairId", $configData.Configuration.ProtectionData.ProtectionPairId)
            break 
        }

        default { }
    }

    return $parameters
}

# Track Method to check output
function TrackForStepCompletion ([string]$step, [PSObject]$output)
{
    $isProcessingLeft = $false

    switch ($cmd)
    {
        "GetMachinesForManagementId" 
        { 
            if(!$output)
            {
                $isProcessingLeft = $true
            }
            break 
        }

        "GetProtectionPairRecoveryPoints" 
        {
            if(!$output)
            {
                $isProcessingLeft = $true
            }
            break 
        }

        default { }
    }

    return $isProcessingLeft
}