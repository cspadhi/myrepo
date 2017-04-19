//---------------------------------------------------------------
//  <copyright file="RemoveProtectionGroupCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Remove-ProtectionGroup cmdlet.
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
    [Cmdlet(VerbsCommon.Remove, "ProtectionGroup", DefaultParameterSetName = RcmParameterSets.Default)]
    public class RemoveProtectionGroupCmdlet : RcmCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public RemoveProtectionGroupInput Input { get; set; }

        /// <summary>
        /// Gets or sets the ID of the protection group.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ProtectionGroupId { get; set; }

        #endregion

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            RemoveProtectionGroupInput removeProtectionGroupInput = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    removeProtectionGroupInput = this.Input;
                    break;

                case RcmParameterSets.Default:
                    removeProtectionGroupInput = this.Load();
                    break;

                default:
                    break;
            }

            if (removeProtectionGroupInput != null)
            {
                this.WriteObject(removeProtectionGroupInput);
                this.RemoveProtectionGroup(removeProtectionGroupInput);
            }
        }

        /// <summary>
        /// Creates RemoveProtectionGroupInput object from input parameters.
        /// </summary>
        /// <returns>RemoveProtectionGroupInput object.</returns>
        private RemoveProtectionGroupInput Load()
        {
            RemoveProtectionGroupInput removeProtectionGroupInput = new RemoveProtectionGroupInput()
            {
                ProtectionGroupId = this.ProtectionGroupId
            };

            return removeProtectionGroupInput;
        }

        /// <summary>
        /// Makes RemoveProtectionGroup call.
        /// </summary>
        /// <param name="removeProtectionGroupInput">RemoveProtectionGroupInput object.</param>
        private void RemoveProtectionGroup(RemoveProtectionGroupInput removeProtectionGroupInput)
        {
            RcmClientForInterService.RemoveProtectionGroup(
                removeProtectionGroupInput,
                PSRcmClient.GetServiceOperationContext());
            this.WriteObject("Protection group removed from RCM.");
        }
    }
}
