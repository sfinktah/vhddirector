/*
 * BSD Licence:
 * Copyright (c) 2001, Lloyd Dupont (lloyd@galador.net)
 * <ORGANIZATION> 
 * All rights reserved.
 * 
 *
 * Redistribution and use in source and binary forms, with or without 
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice, 
 * this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright 
 * notice, this list of conditions and the following disclaimer in the 
 * documentation and/or other materials provided with the distribution.
 * 3. Neither the name of the <ORGANIZATION> nor the names of its contributors
 * may be used to endorse or promote products derived from this software
 * without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE REGENTS OR CONTRIBUTORS BE LIABLE FOR
 * ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT 
 * LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY
 * OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH
 * DAMAGE.
 */

using System;
using System.Collections;
using System.Text;
using System.IO;

namespace VhdDirectorApp {
    /// <summary>
    /// Provides the capability to read and write OpenStep style property
    /// lists. A property list is a nested stucture of <b>ArrayList</b>,
    /// <b>Hashtable</b>, <b>byte[]</b> and <b>string</b>. You could, also,
    /// put (optional) comment in read only string. <br/>
    /// ArrayList are delimited by parentheses with individual elements
    /// separated by commas and optional spaces. <br/>
    /// Hashtables (i.e. NSDictionary objects in OpenStep) are delimited by
    /// curly braces, with key-value pairs separated by semicolons, the
    /// keys and values separated by an equal sign. <br/>
    /// strings appears as plain text if they contain no whitespace, or
    /// enclosed in straight quotation marks '"' if they have whitespace. <br/>
    /// Byte arrays (byte[])(i.e. NSData objects in OpenStep) are delimited
    /// by angled brackets and are encoded as hexadecimal digits. <br/>
    /// It is also a good idea to put comment in read-only plist, to do
    /// it simply put a '#' char, anything from it to end of line will be 
    /// discarded. <br/>
    /// Here's a short example of a text-format property list:
    /// <code>
    /// #
    /// # -- My Super PList !
    /// #
    /// {
    ///     Title = "Star Wars";
    ///     Director = "Lucas, George";
    ///     Cast = (
    ///         "Hamil, Mark",
    ///         "Fisher, Carrier",
    ///         "Ford, Harrison"
    ///         );
    ///     # here my private login info
    ///     "credential" = &lt;040b47f9 
    ///                    # a bigger sets of hexa digits
    ///                    8484074e&gt;
    /// }
    /// </code>
    /// <br/>
    /// This object is a bit tolerant on error, bad separator
    /// and such stuff, but rewrite PList correctly.
    /// </summary>
    /// <see cref="Read"/>
    /// <see cref="Write"/>
    public class PList
  {
    private const string BADCHAR   = "unexpected character '";
    private const string BADEND    = "unexpected end of plist";
    private const string BADOBJECT = " is none of " +
          "Hastable, ArrayList, string or byte[]";
  
#if TEST_PLIST
    internal static long currentTimeMillis() 
    {
      long t = DateTime.UtcNow.Ticks;
      return t / 10000;
    }
    
      /// <summary> test the PList reader/writer </summary>
    public static void Main(string[] args)
    {
      if(args.Length == 0)
        return;
  
      long l0 = currentTimeMillis();
      object o = Read(new StreamReader(new FileStream(
        args[0],
        FileMode.Open)));
      long l1 = currentTimeMillis();
  
      TextWriter w;
      if(args.Length > 1)
        w = new StreamWriter(new FileStream(
          args[1],
          FileMode.CreateNew));
      else
        w = Console.Out;
      Write(w, o);
  
      Console.WriteLine("\ntook " + (l1-l0) + " ms.");
    }
#endif
  
    /** write a PList (Hashtable,ArrayList,string or byte[]) 
     * in a Text file */
    public static void Write(string filename, object plist)
    {
      StreamWriter sw = new StreamWriter(new FileStream(
          filename,
          FileMode.Create));
      Write(sw, plist);
      sw.Close();
    }
    /// <summary> write a PList (Hashtable,ArrayList,string or byte[]) in a Text stream </summary>
    public static void Write(TextWriter w, object plist)
    {
      writeobject(w, 0, plist);
      w.Flush();
    }
    private static void writeobject(TextWriter w, int lvl, object o)
    {
      if(o is Hashtable)
        writedict(w, lvl, (Hashtable) o);
      else if(o is ArrayList)
        writearray(w, lvl, (ArrayList) o);
      else if(o is string)
        writestring(w, (string) o);
      else if(o is byte[])
        writebytes(w, (byte[]) o);
      else
        throw new ArgumentException(o.GetType().FullName +  BADOBJECT);
    }
    private static void printwhite(TextWriter w, int lvl)
    {
      char[] buf = new char[2*lvl];
      for(int i=0; i<buf.Length; i++)
        buf[i] = ' ';
      w.Write(buf);
    }
    private static void writedict(TextWriter w, int lvl, Hashtable h)
    {
      w.Write("{\n");
      ICollection keys = h.Keys;
      foreach(string s in keys) {
        if(s == null)
          continue;
        printwhite(w, lvl+1);
        writestring(w, s);
        w.Write(" = ");
        writeobject(w, lvl+1, h[s]);
        w.Write(";\n");
      }
      printwhite(w, lvl);
      w.Write("}");
    }
    private static void writearray(TextWriter w, int lvl, ArrayList v)
    {
      w.Write("(\n");
      for(int i=0, n=v.Count; i<n; i++) {
        printwhite(w, lvl+1);
        writeobject(w, lvl+1, v[i]);
        if(i != n-1)
          w.Write(",\n");
        else
          w.Write("\n");
      }
      printwhite(w, lvl);
      w.Write(")");
    }
    private static void writestring(TextWriter w, string s)
    {
      w.Write('"');
      for(int i=0; i<s.Length; i++) {
        if(s[i] == '\\' || s[i] == '\'' || s[i] == '"')
          w.Write('\\');
        w.Write(s[i]);
      }
      w.Write('"');
    }
    private static void writebytes(TextWriter w, byte[] b)
    {
      w.Write('<');
      for(int i=0; i<b.Length; i++)
      {
        if(i%4==0 && i!=0 && i!=b.Length-1)
          w.Write(" ");
        w.Write(intToHexa(b[i]));
      }
      w.Write('>');
    }
    private static char[] intToHexa(byte b)
    {
      int n = b < 0 ? b+256 : b;
      char[] tab = new char[2];
      tab[0] = miniToChar(n/16);
      tab[1] = miniToChar(n%16);
      return tab;
    }
    private static char miniToChar(int c)
    {
      if(c < 10)
        return (char)(c + '0');
      else
        return (char)(c + 'A' - 10);
    }
  
    /** read a PList (Hashtable,ArrayList,string or byte[]) 
     * from a Text file
     */
    public static object Read(string filename)
    {
      StreamReader sr = new StreamReader(new FileStream(
          filename,
          FileMode.Open));
      object o = Read(sr);
      sr.Close();
      return o;
    }
    /// <summary> read a PList (Hashtable,ArrayList,string or byte[]) from a Text stream </summary>
    public static object Read(TextReader r)
    {
      return readobject(new PushbackTextReader(r, 1));
    }
    private static object readobject(PushbackTextReader r)
    {
      int c = next(r);
      switch(c)
      {
        case -1:
          return null;
        case '{':
          return readdict(r);
        case '(':
          return readarray(r);
        case '<':
          return readbytes(r);
        default:
          return readstring(r);
      }
    }
    private static string readstring(PushbackTextReader r)
    {
      int first = r.Read();
      if(first == -1)
        throw new ArgumentException(BADEND + ", at line " + r.Line);
      if(breakstring(first))
        throw new ArgumentException(BADCHAR + ((char) first) + "', at line " + r.Line);
  
      StringBuilder sb = new StringBuilder();
      if(first != '\'' && first != '"')
      {
        r.Unread(first);
        first = 0;
      }
  
      int c;
      while(true)
      {
        c = r.Read();
        if(c == '\\') {
          c = r.Read();
          if(c == -1)
            throw new ArgumentException(BADEND + ", at line " + r.Line);
        }
        else if(first != 0 && c == first)
          break;
        else if(first == 0) {
          if(isWhite(c) || c == -1)
            break;
          if(breakstring(c)) {
            r.Unread(c);
            break;
          }
        }
        else if(c == -1)
          throw new ArgumentException(BADEND + ", at line " + r.Line);
        sb.Append((char) c);
      }
      return sb.ToString();
    }
    private static bool breakstring(int c)
    {
      bool ret = c=='=' || c ==',' || c == ';'
        || c =='(' || c ==')' || c =='{' || c =='}'
        || c =='<' || c=='>';
      return ret;
    }
    private static Hashtable readdict(PushbackTextReader r)
    {
      Hashtable h = new Hashtable();
      int c = r.Read();
      if(c != '{')
        throw new ArgumentException(BADCHAR + (char)c + "', at line " + r.Line);
      while(true)
      {
        c = next(r);
        if(c == '}') {
          r.Read();
          break;
        }
        string key = readstring(r);
  
        c = next(r);
        if(c != '=')
          throw new ArgumentException(BADCHAR + (char)c + "', at line " + r.Line);
        r.Read();
  
        next(r);
        object aValue = readobject(r);
        h[key] = aValue;
  
        c = next(r);
        if(c == '}') {
          r.Read();
          break;
        }
        if(c == ';' || c == ',') {
          r.Read();
          continue;
        }
      }
      return h;
    }
    private static ArrayList readarray(PushbackTextReader r)
    {
      ArrayList v = new ArrayList();
      int c = r.Read();
      if(c != '(')
        throw new ArgumentException(BADCHAR + (char)c + "', at line " + r.Line);
      while(true)
      {
        c = next(r);
        if(c == ')') {
          r.Read();
          break;
        }
  
        object item = readobject(r);
        v.Add(item);
  
        c = next(r);
        if(c == ')') {
          r.Read();
          break;
        }
        if(c == ';' || c == ',') {
          r.Read();
          continue;
        }
        // be tolerant
        // throw new ArgumentException(BADCHAR + (char)c + "', at line " + r.Line);
      }
      return v;
    }
    private static byte[] readbytes(PushbackTextReader r)
    {
      MemoryStream bos = new MemoryStream();
      int c = r.Read();
      if(c != '<')
        throw new ArgumentException(BADCHAR + (char)c + "', at line " + r.Line);
  
      char[] tab = new char[2];
      byte[] outTab = new byte[1];
      int Read;
      while(true)
      {
        Read = nextConsumed(r);
        if(Read == -1)
          throw new ArgumentException(BADEND + ", at line " + r.Line);
        if(Read  == '>')
          break;
        tab[0] = (char) Read;
        Read = nextConsumed(r);
        if(Read == -1)
          throw new ArgumentException(BADEND + ", at line " + r.Line);
        tab[1] = (char) Read;
  
        outTab[0] = hexaToInt(tab[0], r);
        outTab[0] <<= 4;
        outTab[0] |= hexaToInt(tab[1], r);
        bos.Write(outTab, 0, 1);
      }
      return bos.ToArray();
    }
    private static byte hexaToInt(char c, PushbackTextReader r)
    {
      int n;
      if('0' <= c && c <= '9')
        n = c - '0';
      else if('a' <= c && c <= 'f')
        n = c - 'a' + 10;
      else if('A' <= c && c <= 'F')
        n = c - 'A' + 10;
      else
        throw new ArgumentException(BADCHAR + (char)c + "', at line " + r.Line);
      return (byte) n;
    }
    private static int next(PushbackTextReader r)
    {
      int c = nextConsumed(r);
      r.Unread(c);
      return c;
    }
    private static int nextConsumed(PushbackTextReader r)
    {
      int c;
      do {
        c = r.Read();
        if(c == '#')
          c = swapComment(r);
      }
      while(isWhite(c));
      return c;
    }
    private static bool isWhite(int c)
    {
      return c == ' ' || c == '\t' || c == '\r' || c == '\n';
    }
    private static int swapComment(PushbackTextReader r)
    {
      int c;
      do c = r.Read();
      while(c != '\n' && c != -1);
      return c;
    }
  }
  
  internal class PushbackTextReader : TextReader
  {
    int[] buf;
    uint index, length;
    TextReader reader;
    int line = 1;
  
    public int Line { get { return line; } }
    public PushbackTextReader(TextReader t, int myBufSize) 
    {
      reader = t;
      buf = new int[myBufSize];
    }
    public override int Peek()
    {
      if(length != 0)
        return buf[index];
      else
        return reader.Peek();
    }
    public override int Read()
    {
      int ret = 0;
      if(length != 0) {
        ret = buf[index];
        index ++;
        length --;
        if(index >= buf.Length)
          index = 0;
      }
      else 
        ret = reader.Read();
      
      if(ret == '\n')
        line ++;
      
      return ret;
    }
    public override int Read(char[] buffer, int anIndex, int count)
    {
      int nread = 0;
      int readed;
      while(length != 0 && count > 0) {
        readed = (int) buf[index ++];
        length --;
        switch(readed) {
          case -1:
            return nread;
          case '\n':
            line ++;
            break;
        }
        buffer[anIndex ++] = (char) readed;
        count --;
        nread ++;
        if(index >= buf.Length)
          index = 0;
      }
      
      nread += reader.Read(buffer, anIndex, count);
      for(int i=count+anIndex; i-->anIndex;)
        if(buffer[i] == '\n')
          line ++;
      
      return nread;
    }
    public virtual void Unread(int c)
    {
      if(c == '\n')
        line --;
      
      index = index == 0 ? (uint) buf.Length - 1 : index --;
      buf[index] = c;
      if(length < buf.Length)
        length ++;
    }
  }
}
