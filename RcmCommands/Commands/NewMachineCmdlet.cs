//---------------------------------------------------------------
//  <copyright file="NewMachineCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for New-Machine cmdlet.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System.Collections.Generic;
using System.Management.Automation;
using RcmClientLib;
using RcmContract;

namespace RcmCommands
{
    /// <summary>
    /// Register machine.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "Machine", DefaultParameterSetName = RcmParameterSets.Default)]
    public class NewMachineCmdlet : RcmCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public RegisterMachineInput Input { get; set; }

        /// <summary>
        /// Gets or sets machine ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the machine friendly name.
        /// FQDN or hostname of the machine.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the machine bios ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string BiosId { get; set; }

        /// <summary>
        /// Gets or sets the ID associated with this entity by the management plane.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ManagementId { get; set; }

        /// <summary>
        /// Gets or sets the type of the operating system installed on the server.
        /// This is a ToString() representation of the
        /// <see cref="RcmEnum.OperatingSystemType"/> enumeration.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string OperatingSystemType { get; set; }

        /// <summary>
        /// Gets or sets name of the operating system.
        /// e.g. RHEL6-64.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string OsName { get; set; }

        /// <summary>
        /// Gets or sets description details of the operating system.
        /// e.g. Microsoft Windows Server 2012 R2 Datacenter.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string OsDescription { get; set; }

        /// <summary>
        /// Gets or sets Architecture details of operating system.
        /// e.g x86/x64.
        /// This is a ToString() representation of the
        /// <see cref="RcmEnum.OsArchitecture"/> enumeration.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string OsArchitecture { get; set; }

        /// <summary>
        /// Gets or sets endian ness of operating system.
        /// e.g Little endian/Big endian.
        /// This is a ToString() representation of the
        /// <see cref="RcmEnum.OsEndianness"/> enumeration.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string OsEndianness { get; set; }

        /// <summary>
        /// Gets or sets operating system major version details.
        /// e.g. in case of RHEL 6U2 - the major version is 6.
        ///      in case of w2k8 R2 - the major version is 2008.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string OsMajorVersion { get; set; }

        /// <summary>
        /// Gets or sets operating system minor version details.
        /// e.g. in case of RHEL 6U3 - the minor version is 3.
        ///      in case of w2k8 R2 - the minor version is 2.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string OsMinorVersion { get; set; }

        /// <summary>
        /// Gets or sets operating system build version details.
        /// e.g. 9200.
        /// Linux - 2.6.32-71.el6.x86_64.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string OsBuildVersion { get; set; }

        /// <summary>
        /// Gets or sets the version of the virtualization platform of the server.
        /// This is a ToString() representation of the
        /// <see cref="RcmEnum.VirtualizationPlatform"/> enumeration.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string VirtualizationPlatform { get; set; }

        /// <summary>
        /// Gets or sets virtualization platform version of the server.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string VirtualizationPlatformVersion { get; set; }

        /// <summary>
        /// Gets or sets the disk extents for operating system volume.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string OsVolumeDiskExtents { get; set; }

        /// <summary>
        /// Gets or sets disk details of the machine.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public List<RegisterMachineInput.DiskInput> DiskInputs { get; set; }

        /// <summary>
        /// Gets or sets partition details of a disk of the machine.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public List<RegisterMachineInput.DiskPartitionInput> PartitionInputs { get; set; }

        /// <summary>
        /// Gets or sets logical volume details of a disk group of the machine.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public List<RegisterMachineInput.LogicalVolumeInput> LogicalVolumes { get; set; }

        /// <summary>
        /// Gets or sets the disk group details of a machine.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public List<RegisterMachineInput.DiskGroupInput> DiskGroups { get; set; }

        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            RegisterMachineInput registerMachineInput = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    registerMachineInput = this.Input;
                    this.Id = this.Input.Id;
                    break;

                case RcmParameterSets.Default:
                    registerMachineInput = this.Load();
                    break;

                default:
                    break;
            }

            if (registerMachineInput != null)
            {
                this.WriteObject(registerMachineInput);
                this.RegisterMachine(registerMachineInput);
            }
        }

        /// <summary>
        /// Creates RegisterMachineInput object from input parameters.
        /// </summary>
        /// <returns>RegisterMachineInput object.</returns>
        private RegisterMachineInput Load()
        {
            RegisterMachineInput registerMachineInput = new RegisterMachineInput()
            {
                Id = this.Id,
                FriendlyName = this.FriendlyName,
                BiosId = this.BiosId,
                ManagementId = this.ManagementId,
                OperatingSystemType = this.OperatingSystemType,
                OsName = this.OsName,
                OsDescription = this.OsDescription,
                OsArchitecture = this.OsArchitecture,
                OsEndianness = this.OsEndianness,
                OsMajorVersion = this.OsMajorVersion,
                OsMinorVersion = this.OsMinorVersion,
                OsBuildVersion = this.OsBuildVersion,
                VirtualizationPlatform = this.VirtualizationPlatform,
                VirtualizationPlatformVersion = this.VirtualizationPlatformVersion,
                OsVolumeDiskExtents = this.OsVolumeDiskExtents,
                DiskInputs = this.DiskInputs,
                PartitionInputs = this.PartitionInputs,
                LogicalVolumes = this.LogicalVolumes,
                DiskGroups = this.DiskGroups
            };

            return registerMachineInput;
        }

        /// <summary>
        /// Makes RegisterMachine call.
        /// </summary>
        /// <param name="registerMachineInput">RegisterMachineInput object.</param>
        private void RegisterMachine(RegisterMachineInput registerMachineInput)
        {
            RcmClientForAgent.RegisterMachine(
                registerMachineInput,
                PSRcmClient.GetAgentOperationContext(this.Id));
            this.WriteObject("Machine registered with RCM.");
        }
    }
}
