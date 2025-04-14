

using Meter.Farm.DTO.Interfaces;
using Meter.Farm.DTO;
using Buffer = Meter.Farm.DTO.Buffer;
using System.Collections.Generic;
using Storage.Layer.Service.Interfaces;

namespace Storage.Layer.Service.Domain
{
    public class MessageProcess : IDataProcess
    {
        private IBuffer _requestBuffer = new Buffer();

        #region Request Commands
        public void AddRequestPackage(ServerPackageObject message)
        {
            _requestBuffer.AddPackage(message);
        }
        public object GetLastRequestPackage()
        {
            if(_requestBuffer.Length() <= 0)
                throw new Exception();

            return (ServerPackageObject)_requestBuffer.GetBuffer()[0];
        }
        public bool IsAvailableRequestPackeage()
        {
            return _requestBuffer.Length() > 0;
        }
        public void ProcessLastRequestPackage()
        {
            _requestBuffer.Pop();
        }
        #endregion




    }
}