//---------------------------------------------------------------
//  <copyright file="NewLogContainerSasUriCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for New-LogContainerSasUri cmdlet.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System.Management.Automation;
using RcmClientLib;

namespace RcmCommands
{
    /// <summary>
    /// Renew log container SAS URI.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "LogContainerSasUri", DefaultParameterSetName = RcmParameterSets.Default)]
    public class NewLogContainerSasUriCmdlet : RcmCmdletBase
    {
        /// <summary>
        /// Gets or sets machine ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string MachineId { get; set; }

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            RcmClientForAgent.RenewLogContainerSasUri(
                PSRcmClient.GetAgentOperationContext(this.MachineId));
            this.WriteObject("Log container SAS URI renewed.");
        }
    }
}
