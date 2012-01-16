package net.loggr.utility;

import java.io.StringWriter;
import java.lang.reflect.Field;
import java.lang.reflect.Member;
import java.lang.reflect.ParameterizedType;
import java.lang.reflect.Type;
import java.lang.reflect.WildcardType;
import java.sql.Date;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;


public class ObjectDumper {
	public static void Write(Object element){
		Write(element, 0);
	}
	
	public static void Write(Object element, int depth)
	{

    	StringWriter log = new StringWriter();
    	
		Write(element, depth, log);
		System.out.println(log.toString());
	}

    public static String dumpObject(Object element, int depth)
    {
    	StringWriter log = new StringWriter();
        Write(element, depth, log);
        return log.toString();
    }

	public static void Write(Object element, int depth, StringWriter out)
	{
		ObjectDumper dumper = new ObjectDumper(depth);
		dumper.writer = out;
		dumper.WriteObject("[Root]", element);
	}
	
	private StringWriter writer;
	private int level;
	
	private int depth;
    private ObjectDumper(int depth)
    {
        this.depth = depth;
    }

    private void Write(String s)
    {
        if (s != null)
        {
            writer.write(s);
            s.length();
        }
    }

    private void WriteLine()
    {
        writer.write("<br/>");
    }

    private void WriteTab()
    {
        Write("&nbsp;");
    }

    private void WriteObject(String prefix, Object element)
    {
        if (element == null || element instanceof String )
        {
            Write(prefix);
            WriteValue(element);
            WriteLine();
        }
        else
        {
            @SuppressWarnings("unchecked")
			Iterable<Object> enumerableElement = this.isIterableClass(element.getClass()) && element instanceof Iterable<?> ? (Iterable<Object>)element : null;
            if (enumerableElement != null)
            {
            	Iterator<Object> itr = enumerableElement.iterator();
                while(itr.hasNext())
                {
                	Object item = itr.next();
                    if (isIterableClass(item.getClass()) && !(item instanceof String))
                    {
                        Write(prefix);
                        Write("...");
                        WriteLine();
                        if (level < depth)
                        {
                            level += 1;
                            WriteObject(prefix, item);
                            level -= 1;
                        }
                    }
                    else
                    {
                        WriteObject(prefix, item);
                    }
                }
            }
            else
            {
            	Class<? extends Object> myClass = element.getClass();
            	Member[] members = myClass.getDeclaredFields();
                Write(prefix);
                WriteLine();
                WriteTab();
                WriteTab();
                boolean propWritten = false;
                for (int i = 0; i < members.length; i++)
                {
                	Field f = (Field)members[i];
                    if (f != null)
                    {
                    	f.setAccessible(true);
                        if (propWritten)
                        {
                            WriteTab();
                        }
                        else
                        {
                            propWritten = true;
                        }
                        Write(f.getName());
                        Write("=");
                        Type t = f.getType();
                        String tester = "";
                        if (t == tester.getClass())
                        {
                            try
                            {
                                WriteValue(f.get(element));
                            }
                            catch (Exception ex)
                            {
                                WriteValue("#ERR");
                            }
                        }
                        else
                        {
                            if (Iterable.class.isAssignableFrom(t.getClass()))
                            {
                                Write("...");
                            }
                            else
                            {
                                Write("{ }");
                            }
                        }
                        WriteLine();
                        WriteTab();
                    }
                }
                if (propWritten)
                {
                    //WriteLine()
                }
                if (level < depth)
                {
                    for (int i = 0; i < members.length; i++)
                    {
                    	Field f = (Field)members[i];
                        if (f != null)
                        {
                            Type t = f.getGenericType();
                            if (!(String.class.equals(t)))
                            {
                                Object value;
								try {
									value = f.get(element);
								} catch (IllegalArgumentException e) {
									// TODO Auto-generated catch block
									continue;
								} catch (IllegalAccessException e) {
									// TODO Auto-generated catch block
									continue;
								}
                                if (value != null)
                                {
                                    level += 1;
                                    WriteObject("[" + f.getName() + "]", value);
                                    level -= 1;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    
    private void WriteValue(Object o)
    {
        if (o == null)
        {
            Write("null");
        }
        else if (Date.class.equals(o))
        {
            Write(((Date)o).toString());
        }
        else if (String.class.equals(o.getClass()))
        {
            String res = (String)o;
            if (res.length() > 150)
            {
                res = res.substring(0, 150) + "...";
            }
            Write(res);
        }
        else if (isIterableClass(o.getClass()))
        {
            Write("...");
        }
        else
        {
            Write("{ }");
        }
    }
    
    
    // Helpers
    
    public boolean isIterable(Type type) {
        if ( type instanceof Class && isIterableClass( ( Class<?> ) type ) ) {
          return true;
        }
        if ( type instanceof ParameterizedType ) {
          return isIterable( ( ( ParameterizedType ) type ).getRawType() );
        }
        if ( type instanceof WildcardType ) {
          Type[] upperBounds = ( ( WildcardType ) type ).getUpperBounds();
          return upperBounds.length != 0 && isIterable( upperBounds[0] );
        }
        return false;
      }
    /**
     * Checks whether the specified class parameter is an instance of a collection class.
     *
     * @param clazz <code>Class</code> to check.
     *
     * @return <code>true</code> is <code>clazz</code> is instance of a collection class, <code>false</code> otherwise.
     */
    private boolean isIterableClass(Class<?> clazz) {
      List<Class<?>> classes = new ArrayList<Class<?>>();
      computeClassHierarchy( clazz, classes );
      return classes.contains( Iterable.class );
    }

    /**
     * Get all superclasses and interfaces recursively.
     *
     * @param clazz The class to start the search with.
     * @param classes List of classes to which to add all found super classes and interfaces.
     */
    private void computeClassHierarchy(Class<?> clazz, List<Class<?>> classes) {
      for ( Class<?> current = clazz; current != null; current = current.getSuperclass() ) {
        if ( classes.contains( current ) ) {
          return;
        }
        classes.add( current );
        for ( Class<?> currentInterface : current.getInterfaces() ) {
          computeClassHierarchy( currentInterface, classes );
        }
      }
    }
    
}
