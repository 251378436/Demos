// ------------------------------------------------------------------------------
// <auto-generated>
//    Generated by avrogen, version 1.11.1
//    Changes to this file may cause incorrect behavior and will be lost if code
//    is regenerated
// </auto-generated>
// ------------------------------------------------------------------------------
namespace vehicle.sensor.massage.replay
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using global::Avro;
	using global::Avro.Specific;
	
	/// <summary>
	/// A simple log message type as used by this blog post.
	/// </summary>
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("avrogen", "1.11.1")]
	public partial class SensorMessageKey : global::Avro.Specific.ISpecificRecord
	{
		public static global::Avro.Schema _SCHEMA = global::Avro.Schema.Parse("{\"type\":\"record\",\"name\":\"SensorMessageKey\",\"doc\":\"A simple log message type as us" +
				"ed by this blog post.\",\"namespace\":\"vehicle.sensor.massage.replay\",\"fields\":[{\"n" +
				"ame\":\"customer_id\",\"type\":\"string\"}]}");
		private string _customer_id;
		public virtual global::Avro.Schema Schema
		{
			get
			{
				return SensorMessageKey._SCHEMA;
			}
		}
		public string customer_id
		{
			get
			{
				return this._customer_id;
			}
			set
			{
				this._customer_id = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.customer_id;
			default: throw new global::Avro.AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.customer_id = (System.String)fieldValue; break;
			default: throw new global::Avro.AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
