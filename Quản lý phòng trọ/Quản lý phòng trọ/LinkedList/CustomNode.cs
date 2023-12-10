using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_phòng_trọ
{
    internal class CustomNode<T>
    {
        public T Data { get; set; }
        public CustomNode<T> Next { get; set; }
        public CustomNode(T data)
        {
            this.Data = data;
        }
    }
}
