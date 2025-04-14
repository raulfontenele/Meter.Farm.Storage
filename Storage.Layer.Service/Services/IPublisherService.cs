using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Layer.Service.Services
{
    public interface IPublisherService
    {
        public void Publish(object package);
    }
}
