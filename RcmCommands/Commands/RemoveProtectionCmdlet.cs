//---------------------------------------------------------------
//  <copyright file="RemoveProtectionCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Remove-Protection cmdlet.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System.Management.Automation;
using RcmClientLib;
using RcmContract;

namespace RcmCommands
{
    /// <summary>
    /// Remove protection group.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "Protection", DefaultParameterSetName = RcmParameterSets.Default)]
    public class RemoveProtectionCmdlet : RcmCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public DisableProtectionInput Input { get; set; }

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Force)]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// Gets or sets the protection pair ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [Parameter(ParameterSetName = RcmParameterSets.Force, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ProtectionPairId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the workflow.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string WorkflowId { get; set; }

        #endregion

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            if (this.Force)
            {
                PurgeProtectionInput purgeProtectionInput = this.Load(this.ProtectionPairId);
                this.WriteObject(purgeProtectionInput);
                this.PurgeProtection(purgeProtectionInput);
            }
            else
            {
                DisableProtectionInput disableProtectionInput = null;
                switch (this.ParameterSetName)
                {
                    case RcmParameterSets.ByObject:
                        disableProtectionInput = this.Input;
                        break;

                    case RcmParameterSets.Default:
                        disableProtectionInput = this.Load();
                        break;

                    default:
                        break;
                }

                if (disableProtectionInput != null)
                {
                    this.WriteObject(disableProtectionInput);
                    this.DisableProtection(disableProtectionInput);
                }
            }
        }

        /// <summary>
        /// Creates PurgeProtectionInput object from input parameters.
        /// </summary>
        /// <param name="protectionPairId">Protection pair identity.</param>
        /// <returns>PurgeProtectionInput object.</returns>
        private PurgeProtectionInput Load(string protectionPairId)
        {
            PurgeProtectionInput purgeProtectionInput = new PurgeProtectionInput()
            {
                ProtectionPairId = protectionPairId
            };

            return purgeProtectionInput;
        }

        /// <summary>
        /// Creates DisableProtectionInput object from input parameters.
        /// </summary>
        /// <returns>DisableProtectionInput object.</returns>
        private DisableProtectionInput Load()
        {
            DisableProtectionInput disableProtectionInput = new DisableProtectionInput()
            {
                ProtectionPairId = this.ProtectionPairId,
                WorkflowId = this.WorkflowId
            };

            return disableProtectionInput;
        }

        /// <summary>
        /// Makes DisableProtection call.
        /// </summary>
        /// <param name="disableProtectionInput">DisableProtectionInput object.</param>
        private void DisableProtection(DisableProtectionInput disableProtectionInput)
        {
            RcmWorkflowDetails rcmWorkflowDetails = RcmClientForInterService.DisableProtection(
                disableProtectionInput,
                PSRcmClient.GetServiceOperationContext());

            this.WriteObject(rcmWorkflowDetails);
        }

        /// <summary>
        /// Makes PurgeProtection call.
        /// </summary>
        /// <param name="purgeProtectionInput">PurgeProtectionInput object.</param>
        private void PurgeProtection(PurgeProtectionInput purgeProtectionInput)
        {
            RcmClientForInterService.PurgeProtection(
                purgeProtectionInput,
                PSRcmClient.GetServiceOperationContext());
            this.WriteObject("Protection removed from RCM.");
        }
    }
}
