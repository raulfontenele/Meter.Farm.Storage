using Meter.Farm.DTO;
using Meter.Farm.DTO.Repository;

namespace Storage.Layer.Service.Interfaces
{
    public interface IDataProcess
    {
        public bool IsAvailableRequestPackeage();
        public object GetLastRequestPackage();
        public void ProcessLastRequestPackage();
        public void AddRequestPackage(StorageCommandObjectRequest message);
        public IList<StorageCommandObjectRequest> GetRequestPackageList();
        public void ClearRequestBuffer();

    }
}