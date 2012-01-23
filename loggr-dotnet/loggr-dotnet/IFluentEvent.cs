using System;
using System.Collections.Generic;
using System.Text;

namespace Loggr
{
    public interface IFluentEvent<T>
    {
        T Text(string Text);
        T Link(string Link);
        T Source(string Source);
        T User(string User);
        T Timestamp(DateTime Timestamp);
        T Tags(string Tags);
        T Value(double Value);
        T Data(string Data);
        T DataType(DataType type);

        Event Event { get; }
    }
}
