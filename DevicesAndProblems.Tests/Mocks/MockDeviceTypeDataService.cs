﻿using DevicesAndProblems.App.Services;
using DevicesAndProblems.Model;
using System.Collections.Generic;

namespace DevicesAndProblems.Tests
{
    public class MockDeviceTypeDataService : IDeviceTypeDataService
    {
        private MockRepository repository = new MockRepository();

        public List<DeviceType> GetAllDeviceTypes()
        {
            return repository.GetDeviceTypes();
        }

        public void UpdateDeviceType(DeviceType newDeviceType, int selectedDeviceTypeId)
        {
            repository.UpdateDeviceType(newDeviceType, selectedDeviceTypeId);
        }

        public void AddDeviceType(DeviceType newDeviceType)
        {
            repository.AddDeviceType(newDeviceType);
        }

        public void DeleteDeviceType(DeviceType deviceType)
        {
        }

        public List<Device> GetDevicesOfDeviceType(int id)
        {
            return repository.GetDevicesOfDeviceType(id);
        }
    }
}
