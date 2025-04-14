using Meter.Farm.DTO;

namespace Storage.Layer.Service.Interfaces
{
    public interface IDataProcess
    {
        public bool IsAvailableRequestPackeage();
        public object GetLastRequestPackage();
        public void ProcessLastRequestPackage();
        public void AddRequestPackage(ServerPackageObject message);
        public IList<ServerPackageObject> GetRequestPackageList();
        public void ClearRequestBuffer();

    }
}