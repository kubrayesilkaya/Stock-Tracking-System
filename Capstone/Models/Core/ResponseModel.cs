namespace Capstone.Models.Core
{
    public class ResponseModel<T>
    {
        public bool Result { get; set; }

        public string Message { get; set; }

        private T _data;
        public T Data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}
