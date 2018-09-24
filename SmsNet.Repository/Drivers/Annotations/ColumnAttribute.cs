using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Repository.Data.Annotations
{
	[AttributeUsage(AttributeTargets.Property,AllowMultiple =false,Inherited =true)]
	public class ColumnAttribute : Attribute
	{
		public string Name { get; set; }
		public ColumnAttribute(string name)
		{
			this.Name = name;
		}
	}
}
