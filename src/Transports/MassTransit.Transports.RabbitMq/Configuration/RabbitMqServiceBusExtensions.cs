﻿// Copyright 2007-2011 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit
{
	using System;
	using EndpointConfigurators;
	using Transports.RabbitMq;
	using Transports.RabbitMq.Configuration.Configurators;

	public static class RabbitMqServiceBusExtensions
	{
		/// <summary>
		/// Returns the endpoint for the specified message type using the default
		/// exchange/queue convention for naming
		/// </summary>
		/// <typeparam name="TMessage">The message type to convert to a URI</typeparam>
		/// <param name="bus">The bus instance used to resolve the endpoint</param>
		/// <returns>The IEndpoint instance, resolved from the service bus</returns>
		public static IEndpoint GetEndpoint<TMessage>(this IServiceBus bus)
			where TMessage : class
		{
			return GetEndpoint(bus, typeof (TMessage));
		}

		/// <summary>
		/// Returns the endpoint for the specified message type using the default
		/// exchange/queue convention for naming
		/// </summary>
		/// <param name="bus">The bus instance used to resolve the endpoint</param>
		/// <param name="messageType">The message type to convert to a URI</param>
		/// <returns>The IEndpoint instance, resolved from the service bus</returns>
		public static IEndpoint GetEndpoint(this IServiceBus bus, Type messageType)
		{
			return null;
		}

		public static T UseRabbitMq<T>(this T configurator)
			where T : EndpointFactoryConfigurator
		{
			var transportFactoryConfigurator = new RabbitMqTransportFactoryConfiguratorImpl();

			configurator.AddTransportFactory(transportFactoryConfigurator.Build);

			configurator.UseJsonSerializer();

			return configurator;
		}

		public static T UseRabbitMq<T>(this T configurator, Action<RabbitMqTransportFactoryConfigurator> configureFactory)
			where T : EndpointFactoryConfigurator
		{
			var transportFactoryConfigurator = new RabbitMqTransportFactoryConfiguratorImpl();

			configureFactory(transportFactoryConfigurator);

			configurator.AddTransportFactory(transportFactoryConfigurator.Build);

			configurator.UseJsonSerializer();

			return configurator;
		}
	}
}