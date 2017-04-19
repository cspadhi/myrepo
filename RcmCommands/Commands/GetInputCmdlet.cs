//---------------------------------------------------------------
//  <copyright file="GetInputCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Get-Input cmdlet.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using RcmContract;

namespace RcmCommands
{
    /// <summary>
    /// Get Input.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "Input", DefaultParameterSetName = RcmParameterSets.Default)]
    [OutputType(typeof(PSObject))]
    public class GetInputCmdlet : RcmCmdletBase
    {
        /// <summary>
        /// Gets or sets the source file path.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string SourceFilePath { get; set; }

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            string filePath = this.SourceFilePath;
            string fileName = Path.GetFileName(filePath);

            if (string.Compare(filePath, fileName, StringComparison.OrdinalIgnoreCase) == 0)
            {
                filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Inputs\\" + filePath;
            }

            string className = Path.GetFileNameWithoutExtension(filePath);

            switch (className)
            {
                //// Control plane APIs Input

                case "RegisterIdentityInput":
                    var registerIdentityInput = Utilities.GetObjectFromJson<RegisterIdentityInput>(filePath);
                    this.WriteObject(registerIdentityInput);
                    break;

                case "UnregisterIdentityInput":
                    var unregisterIdentityInput = Utilities.GetObjectFromJson<UnregisterIdentityInput>(filePath);
                    this.WriteObject(unregisterIdentityInput);
                    break;

                case "CreateProtectionGroupInput":
                    var createProtectionGroupInput = Utilities.GetObjectFromJson<CreateProtectionGroupInput>(filePath);
                    this.WriteObject(createProtectionGroupInput);
                    break;

                case "ModifyProtectionGroupInput":
                    var modifyProtectionGroupInput = Utilities.GetObjectFromJson<ModifyProtectionGroupInput>(filePath);
                    this.WriteObject(modifyProtectionGroupInput);
                    break;

                case "EnableProtectionToAzureInput":
                    var enableProtectionToAzureInput = Utilities.GetObjectFromJson<EnableProtectionToAzureInput>(filePath);
                    this.WriteObject(enableProtectionToAzureInput);
                    break;

                case "StartReplicationInput":
                    var startReplicationInput = Utilities.GetObjectFromJson<StartReplicationInput>(filePath);
                    this.WriteObject(startReplicationInput);
                    break;

                case "DisableProtectionInput":
                    var disableProtectionInput = Utilities.GetObjectFromJson<DisableProtectionInput>(filePath);
                    this.WriteObject(disableProtectionInput);
                    break;

                case "PurgeProtectionInput":
                    var purgeProtectionInput = Utilities.GetObjectFromJson<PurgeProtectionInput>(filePath);
                    this.WriteObject(purgeProtectionInput);
                    break;

                case "RemoveProtectionGroupInput":
                    var removeProtectionGroupInput = Utilities.GetObjectFromJson<RemoveProtectionGroupInput>(filePath);
                    this.WriteObject(removeProtectionGroupInput);
                    break;

                case "StartResyncInput":
                    var startResyncInput = Utilities.GetObjectFromJson<StartResyncInput>(filePath);
                    this.WriteObject(startResyncInput);
                    break;

                case "GetRecoveryPointDetailsForAzureInput":
                    var getRecoveryPointDetailsForAzureInput = Utilities.GetObjectFromJson<GetRecoveryPointDetailsForAzureInput>(filePath);
                    this.WriteObject(getRecoveryPointDetailsForAzureInput);
                    break;

                case "GetProtectionPairRecoveryPointsForAzureInput":
                    var getProtectionPairRecoveryPointsForAzureInput = Utilities.GetObjectFromJson<GetProtectionPairRecoveryPointsForAzureInput>(filePath);
                    this.WriteObject(getProtectionPairRecoveryPointsForAzureInput);
                    break;

                case "GetProtectionGroupRecoveryPointsForAzureInput":
                    var getProtectionGroupRecoveryPointsForAzureInput = Utilities.GetObjectFromJson<GetProtectionGroupRecoveryPointsForAzureInput>(filePath);
                    this.WriteObject(getProtectionGroupRecoveryPointsForAzureInput);
                    break;

                case "HydrationToAzureInput":
                    var hydrationToAzureInput = Utilities.GetObjectFromJson<HydrationToAzureInput>(filePath);
                    this.WriteObject(hydrationToAzureInput);
                    break;

                case "StartFailoverToAzureInput":
                    var startFailoverToAzureInput = Utilities.GetObjectFromJson<StartFailoverToAzureInput>(filePath);
                    this.WriteObject(startFailoverToAzureInput);
                    break;

                case "StartTestFailoverToAzureInput":
                    var startTestFailoverToAzureInput = Utilities.GetObjectFromJson<StartTestFailoverToAzureInput>(filePath);
                    this.WriteObject(startTestFailoverToAzureInput);
                    break;

                //// Gateway APIs Input

                case "RegisterGatewayServiceInput":
                    var registerGatewayServiceInput = Utilities.GetObjectFromJson<RegisterGatewayServiceInput>(filePath);
                    this.WriteObject(registerGatewayServiceInput);
                    break;

                case "CompleteResyncInput":
                    var completeResyncInput = Utilities.GetObjectFromJson<CompleteResyncInput>(filePath);
                    this.WriteObject(completeResyncInput);
                    break;

                case "GatewaySetResyncRequiredInput":
                    var gatewaySetResyncRequiredInput = Utilities.GetObjectFromJson<GatewaySetResyncRequiredInput>(filePath);
                    this.WriteObject(gatewaySetResyncRequiredInput);
                    break;

                case "BaselineBookmarkUpdateInput":
                    var baselineBookmarkUpdateInput = Utilities.GetObjectFromJson<BaselineBookmarkUpdateInput>(filePath);
                    this.WriteObject(baselineBookmarkUpdateInput);
                    break;
                
                //// Agent APIs Input

                case "RegisterMachineInput":
                    var registerMachineInput = Utilities.GetObjectFromJson<RegisterMachineInput>(filePath);
                    this.WriteObject(registerMachineInput);
                    break;

                case "RegisterSourceAgentInput":
                    var registerSourceAgentInput = Utilities.GetObjectFromJson<RegisterSourceAgentInput>(filePath);
                    this.WriteObject(registerSourceAgentInput);
                    break;

                default:

                    this.WriteObject("Invalid XML file name.");
                    break;
            }
        }
    }
}
