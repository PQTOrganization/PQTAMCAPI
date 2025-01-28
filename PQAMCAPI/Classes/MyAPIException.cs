using System;

namespace API.Classes
{
    public class MyAPIException : Exception
    {
        public MyAPIException()
        {
        }

        public MyAPIException(string message): base(message)
        {
        }

        public MyAPIException(string message, Exception inner): base(message, inner)
        {
        }
    }
}
