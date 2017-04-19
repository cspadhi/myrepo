//---------------------------------------------------------------
//  <copyright file="StopFailoverToAzureCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Stop-FailoverToAzure cmdlet.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System.Management.Automation;
using RcmClientLib;

namespace RcmCommands
{
    /// <summary>
    /// Cancel failover to Azure.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Stop, "FailoverToAzure", DefaultParameterSetName = RcmParameterSets.Default)]
    public class StopFailoverToAzureCmdlet : RcmCmdletBase
    {
        /// <summary>
        /// Gets or sets the protection pair ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ProtectionPairId { get; set; }

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            RcmClientForInterService.CancelFailoverToAzure(
                this.ProtectionPairId,
                PSRcmClient.GetServiceOperationContext());
            this.WriteObject("Failover to Azure cancelled.");
        }
    }
}
