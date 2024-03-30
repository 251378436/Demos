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
	public partial class SensorMessageValue : global::Avro.Specific.ISpecificRecord
	{
		public static global::Avro.Schema _SCHEMA = global::Avro.Schema.Parse(@"{""type"":""record"",""name"":""SensorMessageValue"",""doc"":""A simple log message type as used by this blog post."",""namespace"":""vehicle.sensor.massage.replay"",""fields"":[{""name"":""vehicle_id"",""type"":""string""},{""name"":""customer"",""type"":{""type"":""record"",""name"":""Customer"",""namespace"":""vehicle.sensor.massage.replay"",""fields"":[{""name"":""customerI_id"",""type"":""string""},{""name"":""description"",""type"":""string""},{""name"":""customer_type"",""type"":{""type"":""enum"",""name"":""CustomerType"",""doc"":""Enumerates the customer types."",""namespace"":""vehicle.sensor.massage.replay"",""symbols"":[""Standard"",""Premium"",""Expired""],""default"":""Standard""}}]}},{""name"":""tags"",""type"":{""type"":""map"",""values"":""string""}}]}");
		private string _vehicle_id;
		private vehicle.sensor.massage.replay.Customer _customer;
		private IDictionary<string,System.String> _tags;
		public virtual global::Avro.Schema Schema
		{
			get
			{
				return SensorMessageValue._SCHEMA;
			}
		}
		public string vehicle_id
		{
			get
			{
				return this._vehicle_id;
			}
			set
			{
				this._vehicle_id = value;
			}
		}
		public vehicle.sensor.massage.replay.Customer customer
		{
			get
			{
				return this._customer;
			}
			set
			{
				this._customer = value;
			}
		}
		public IDictionary<string,System.String> tags
		{
			get
			{
				return this._tags;
			}
			set
			{
				this._tags = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.vehicle_id;
			case 1: return this.customer;
			case 2: return this.tags;
			default: throw new global::Avro.AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.vehicle_id = (System.String)fieldValue; break;
			case 1: this.customer = (vehicle.sensor.massage.replay.Customer)fieldValue; break;
			case 2: this.tags = (IDictionary<string,System.String>)fieldValue; break;
			default: throw new global::Avro.AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}