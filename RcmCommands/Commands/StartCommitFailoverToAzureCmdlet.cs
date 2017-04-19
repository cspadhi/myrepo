//---------------------------------------------------------------
//  <copyright file="StartCommitFailoverToAzureCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Stop-CommitFailoverToAzure cmdlet.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System.Management.Automation;
using RcmClientLib;

namespace RcmCommands
{
    /// <summary>
    /// Commit failover to Azure.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "CommitFailoverToAzure", DefaultParameterSetName = RcmParameterSets.Default)]
    public class StartCommitFailoverToAzureCmdlet : RcmCmdletBase
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

            RcmClientForInterService.CommitFailoverToAzure(
                this.ProtectionPairId,
                PSRcmClient.GetServiceOperationContext());
            this.WriteObject("Failover to Azure committed.");
        }
    }
}
