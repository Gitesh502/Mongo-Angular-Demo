using KnowCostData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowCostData.ResponseDTO
{
    public class SingleResponseDTO<T>
    {
        
            private bool IsValid = false;
            public T Item { get; set; }
            public bool isValid { get { return IsValid; } set { this.IsValid = value; } }
            public int Id { get; set; }
        
    }
    public class ListResponseDTO<T>
    {

        private bool IsValid = false;
        public List<T> Items { get; set; }
        public bool isValid { get { return IsValid; } set { this.IsValid = value; } }
        public int Id { get; set; }

    }
    public class ResponseDTO
    {

        private bool IsValid = false;
        public bool isValid { get { return IsValid; } set { this.IsValid = value; } }
        public int Id { get; set; }

    }
    public class UserResponseDTO
    {
        private bool IsValid = false;
        public bool isValid { get { return IsValid; } set { this.IsValid = value; } }
        public int Id { get; set; }
    }

    public class UserSingleResponseDTO
    {
        private bool IsValid = false;
        public users Users { get; set; }
        public bool isValid { get { return IsValid; } set { this.IsValid = value; } }
        public int Id { get; set; }
    }

}
