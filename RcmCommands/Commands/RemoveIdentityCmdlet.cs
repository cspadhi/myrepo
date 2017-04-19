//---------------------------------------------------------------
//  <copyright file="RemoveIdentityCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Remove-Identity cmdlet.
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
    /// Unregister identity.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "Identity", DefaultParameterSetName = RcmParameterSets.Default)]
    public class RemoveIdentityCmdlet : RcmCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public UnregisterIdentityInput Input { get; set; }

        /// <summary>
        /// Gets or sets the Tenant ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string TenantId { get; set; }

        /// <summary>
        /// Gets or sets the Object ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ObjectId { get; set; }

        #endregion

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            UnregisterIdentityInput unregisterIdentityInput = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    unregisterIdentityInput = this.Input;
                    break;

                case RcmParameterSets.Default:
                    unregisterIdentityInput = this.Load();
                    break;

                default:
                    break;
            }

            if (unregisterIdentityInput != null)
            {
                this.WriteObject(unregisterIdentityInput);
                this.UnregisterIdentity(unregisterIdentityInput);
            }
        }

        /// <summary>
        /// Creates UnregisterIdentityInput object from input parameters.
        /// </summary>
        /// <returns>UnregisterIdentityInput object.</returns>
        private UnregisterIdentityInput Load()
        {
            UnregisterIdentityInput unregisterIdentityInput = new UnregisterIdentityInput()
            {
                TenantId = this.TenantId,
                ObjectId = this.ObjectId
            };

            return unregisterIdentityInput;
        }

        /// <summary>
        /// Makes UnregisterIdentity call.
        /// </summary>
        /// <param name="unregisterIdentityInput">UnregisterIdentityInput object.</param>
        private void UnregisterIdentity(UnregisterIdentityInput unregisterIdentityInput)
        {
            RcmClientForInterService.UnregisterIdentity(
                unregisterIdentityInput,
                PSRcmClient.GetServiceOperationContext());
            this.WriteObject("Identity unregistered from RCM.");
        }
    }
}
