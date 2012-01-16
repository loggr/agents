package net.loggr;

import net.loggr.utility.*;

public class Events 
{
	private Events(){
		
	}

    public static FluentEvent create()
    {
        return new FluentEvent();
    }


    public static FluentEvent createFromException(Exception ex)
    {
        return createFromException(ex, null);
    }

    public static FluentEvent createFromException(Exception ex, Object traceObject)
    {
        return create()
            .text(ex.getMessage())
            .tags("error " + ExceptionFormatter.FormatType(ex))
            .source(ex.getClass() == null ? "" : ex.getClass().toString())
            .data(ExceptionFormatter.Format(ex, traceObject))
            .dataType(DataType.html);
    }

    public static FluentEvent createFromVariable(Object traceObject)
    {
        return createFromVariable(traceObject, 1);
    }

    public static FluentEvent createFromVariable(Object traceObject, int depth)
    {
        return create()
                .data(ObjectDumper.dumpObject(traceObject, depth))
                .dataType(DataType.html);
    }
}
