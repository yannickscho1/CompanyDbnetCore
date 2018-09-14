using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNetCore.Helper
{

    [Serializable]
    // 1. Möglichkeit
    public class RepoException : Exception
    {
        public UpdateResultType Type { get; set; }
        public RepoException(UpdateResultType type) {
            Type = type;
        }
        public RepoException(string message, UpdateResultType type) : base(message) {
            Type = type;
        }
        public RepoException(string message, Exception inner) : base(message, inner) { }
        protected RepoException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    // 2. Möglichkeit
    public class RepoException<T> : Exception
    {
        public T Type { get; set; }
        public RepoException(T type)
        {
            Type = type;
        }
        public RepoException(string message, T type) : base(message)
        {
            Type = type;
        }
        public RepoException(string message, Exception inner) : base(message, inner) { }
        protected RepoException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public enum UpdateResultType
    {
        OK =1,
        SQLERROR,
        NOTFOUND,
        INVALIDEARGUMENT,
        ERROR
   
    }
}
