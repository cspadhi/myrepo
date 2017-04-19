//---------------------------------------------------------------
//  <copyright file="CompleteResyncCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Complete-Resync cmdlet.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Management.Automation;
using RcmClientLib;
using RcmContract;

namespace RcmCommands
{
    /// <summary>
    /// Set complete resync.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Complete, "Resync", DefaultParameterSetName = RcmParameterSets.Default)]
    public class CompleteResyncCmdlet : RcmCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public CompleteResyncInput Input { get; set; }

        /// <summary>
        /// Gets or sets the source machine identifier.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string SourceMachineId { get; set; }

        /// <summary>
        /// Gets or sets the source disk identifier.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string SourceDiskId { get; set; }

        /// <summary>
        /// Gets or sets the target server.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string TargetServer { get; set; }

        /// <summary>
        /// Gets or sets the target disk identifier.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string TargetDiskId { get; set; }

        /// <summary>
        /// Gets or sets the resync session id.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ResyncSessionId { get; set; }

        /// <summary>
        /// Gets or sets the resync completion status.
        /// This is a ToString() representation of the
        /// <see cref="RcmEnum.ResyncCompletionStatus"/> enumeration.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets ratio of the matched bytes.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public int MatchedBytesRatio { get; set; }

        /// <summary>
        /// Gets or sets time taken for resync copy job execution.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public TimeSpan ElapsedTime { get; set; }

        /// <summary>
        /// Gets or sets errors for resync copy job.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default)]
        public List<RcmJobError> Errors { get; set; }

        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            CompleteResyncInput completeResyncInput = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    completeResyncInput = this.Input;
                    break;

                case RcmParameterSets.Default:
                    completeResyncInput = this.Load();
                    break;

                default:
                    break;
            }

            if (completeResyncInput != null)
            {
                this.WriteObject(completeResyncInput);
                this.CompleteResync(completeResyncInput);
            }
        }

        /// <summary>
        /// Creates CompleteResyncInput object from input parameters.
        /// </summary>
        /// <returns>CompleteResyncInput object.</returns>
        private CompleteResyncInput Load()
        {
            CompleteResyncInput completeResyncInput = new CompleteResyncInput()
            {
                SourceMachineId = this.SourceMachineId,
                SourceDiskId = this.SourceDiskId,
                TargetServer = this.TargetServer,
                TargetDiskId = this.TargetDiskId,
                Status = this.Status,
                MatchedBytesRatio = this.MatchedBytesRatio,
                ElapsedTime = this.ElapsedTime
            };

            if (this.Errors != null)
            {
                completeResyncInput.Errors = this.Errors;
            }

            return completeResyncInput;
        }

        /// <summary>
        /// Makes CompleteResync call.
        /// </summary>
        /// <param name="completeResyncInput">CompleteResyncInput object.</param>
        private void CompleteResync(CompleteResyncInput completeResyncInput)
        {
            RcmClientForInterService.CompleteResync(
                completeResyncInput,
                PSRcmClient.GetServiceOperationContext());
            this.WriteObject("Resync completion status set in RCM.");
        }
    }
}
