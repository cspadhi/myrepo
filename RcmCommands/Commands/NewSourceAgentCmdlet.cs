//---------------------------------------------------------------
//  <copyright file="NewSourceAgentCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for New-SourceAgent cmdlet.
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
    /// Register source agent.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "SourceAgent", DefaultParameterSetName = RcmParameterSets.Default)]
    public class NewSourceAgentCmdlet : RcmCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public RegisterSourceAgentInput Input { get; set; }

        /// <summary>
        /// Gets or sets machine ID of the source agent.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Friendly name of the source agent.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the version of the source agent.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string AgentVersion { get; set; }

        /// <summary>
        /// Gets or sets the filter driver version of the source agent.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string DriverVersion { get; set; }

        /// <summary>
        /// Gets or sets the installation location of the source agent.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string InstallationLocation { get; set; }

        #endregion

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            RegisterSourceAgentInput registerSourceAgentInput = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    registerSourceAgentInput = this.Input;
                    this.Id = this.Input.Id;
                    break;

                case RcmParameterSets.Default:
                    registerSourceAgentInput = this.Load();
                    break;

                default:
                    break;
            }

            if (registerSourceAgentInput != null)
            {
                this.WriteObject(registerSourceAgentInput);
                this.RegisterSourceAgent(registerSourceAgentInput);
            }
        }

        /// <summary>
        /// Creates RegisterSourceAgentInput object from input parameters.
        /// </summary>
        /// <returns>RegisterSourceAgentInput object.</returns>
        private RegisterSourceAgentInput Load()
        {
            RegisterSourceAgentInput registerSourceAgentInput = new RegisterSourceAgentInput()
            {
                Id = this.Id,
                FriendlyName = this.FriendlyName,
                AgentVersion = this.AgentVersion,
                DriverVersion = this.DriverVersion,
                InstallationLocation = this.InstallationLocation
            };

            return registerSourceAgentInput;
        }

        /// <summary>
        /// Makes RegisterSourceAgent call.
        /// </summary>
        /// <param name="registerSourceAgentInput">RegisterSourceAgentInput object.</param>
        private void RegisterSourceAgent(RegisterSourceAgentInput registerSourceAgentInput)
        {
            RcmClientForAgent.RegisterSourceAgent(
                registerSourceAgentInput,
                PSRcmClient.GetAgentOperationContext(this.Id));
            this.WriteObject("Source agent registered with RCM.");
        }
    }
}
