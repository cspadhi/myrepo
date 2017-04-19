//---------------------------------------------------------------
//  <copyright file="NewGatewayServiceCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for New-GatewayService cmdlet.
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
    /// Register gateway service.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "GatewayService", DefaultParameterSetName = RcmParameterSets.Default)]
    public class NewGatewayServiceCmdlet : RcmCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public RegisterGatewayServiceInput Input { get; set; }

        /// <summary>
        /// Gets or sets the gateway service ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of queue used for communicating with the gateway service.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string QueueName { get; set; }

        /// <summary>
        /// Gets or sets the connection string secret name to be used for communicating
        /// with the gateway service.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ServiceBusConnectionStringSecretName { get; set; }

        /// <summary>
        /// Gets or sets the azure key vault URI to read the connection string from.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string AzureKeyVaultUri { get; set; }

        #endregion

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            RegisterGatewayServiceInput registerGatewayServiceInput = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    registerGatewayServiceInput = this.Input;
                    break;

                case RcmParameterSets.Default:
                    registerGatewayServiceInput = this.Load();
                    break;

                default:
                    break;
            }

            if (registerGatewayServiceInput != null)
            {
                this.WriteObject(registerGatewayServiceInput);
                this.RegisterGatewayService(registerGatewayServiceInput);
            }
        }

        /// <summary>
        /// Creates RegisterGatewayServiceInput object from input parameters.
        /// </summary>
        /// <returns>RegisterGatewayServiceInput object.</returns>
        private RegisterGatewayServiceInput Load()
        {
            RegisterGatewayServiceInput registerGatewayServiceInput =
                new RegisterGatewayServiceInput()
                {
                    Id = this.Id,
                    QueueName = this.QueueName,
                    ServiceBusConnectionStringSecretName = this.ServiceBusConnectionStringSecretName,
                    AzureKeyVaultUri = this.AzureKeyVaultUri
                };

            return registerGatewayServiceInput;
        }

        /// <summary>
        /// Makes RegisterGatewayService call.
        /// </summary>
        /// <param name="registerGatewayServiceInput">RegisterGatewayServiceInput object.</param>
        private void RegisterGatewayService(RegisterGatewayServiceInput registerGatewayServiceInput)
        {
            RcmClientForInterService.RegisterGatewayService(
                registerGatewayServiceInput,
                PSRcmClient.GetServiceOperationContext());
            this.WriteObject("Gateway service got registered from RCM.");
        }
    }
}
