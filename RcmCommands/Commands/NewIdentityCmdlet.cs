//---------------------------------------------------------------
//  <copyright file="NewIdentityCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for New-Identity cmdlet.
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
    /// Register identity.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "Identity", DefaultParameterSetName = RcmParameterSets.Default)]
    public class NewIdentityCmdlet : RcmCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public RegisterIdentityInput Input { get; set; }

        /// <summary>
        /// Gets or sets the Resource Location.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ResourceLocation { get; set; }

        /// <summary>
        /// Gets or sets the Resource ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the Container ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ContainerId { get; set; }

        /// <summary>
        /// Gets or sets the Container Unique Name.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ContainerUniqueName { get; set; }

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

        /// <summary>
        /// Gets or sets the Audience.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets the protection service URI.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ProtectionServiceUri { get; set; }

        #endregion

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            RegisterIdentityInput registerIdentityInput = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    registerIdentityInput = this.Input;
                    break;

                case RcmParameterSets.Default:
                    registerIdentityInput = this.Load();
                    break;

                default:
                    break;
            }

            if (registerIdentityInput != null)
            {
                this.WriteObject(registerIdentityInput);
                this.RegisterIdentity(registerIdentityInput);
            }
        }

        /// <summary>
        /// Creates RegisterIdentityInput object from input parameters.
        /// </summary>
        /// <returns>RegisterIdentityInput object.</returns>
        private RegisterIdentityInput Load()
        {
            RegisterIdentityInput registerIdentityInput = new RegisterIdentityInput()
            {
                ResourceLocation = this.ResourceLocation,
                ResourceId = this.ResourceId,
                ContainerId = this.ContainerId,
                ContainerUniqueName = this.ContainerUniqueName,
                TenantId = this.TenantId,
                ObjectId = this.ObjectId,
                Audience = this.Audience,
                ProtectionServiceUri = this.ProtectionServiceUri
            };

            return registerIdentityInput;
        }

        /// <summary>
        /// Makes RegisterIdentity call.
        /// </summary>
        /// <param name="registerIdentityInput">RegisterIdentityInput object.</param>
        private void RegisterIdentity(RegisterIdentityInput registerIdentityInput)
        {
            RcmClientForInterService.RegisterIdentity(
                registerIdentityInput,
                PSRcmClient.GetServiceOperationContext());
            this.WriteObject("Identity registered with RCM.");
        }
    }
}
