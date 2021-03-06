﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Transactions;
using NHibernate.Dialect;
using NHibernate.Impl;
using NUnit.Framework;
using NHibernate.Criterion;

namespace NHibernate.Test.NHSpecificTest.NH2113
{
	using System.Threading.Tasks;
	[TestFixture]
	public class FixtureAsync : BugTestCase
	{

        [Test]
        public async Task ShouldNotEagerLoadKeyManyToOneWhenOverridingGetHashCodeAsync()
        {
            using (var s = OpenSession())
            using(var tx = s.BeginTransaction())
            {
                var grp = new Group();
                await (s.SaveAsync(grp));

                var broker = new Broker{Key = new Key{BankId = 1, Id = -1}};
                await (s.SaveAsync(broker));

                var load = new Loan {Broker = broker, Group = grp, Name = "money!!!"};
                await (s.SaveAsync(load));

                await (tx.CommitAsync());
            }

            bool isInitialized;
            using (var s = OpenSession())
            using (var tx = s.BeginTransaction())
            {
                var loan = await (s.CreateCriteria<Loan>()
                    .UniqueResultAsync<Loan>());

                isInitialized = NHibernateUtil.IsInitialized(loan.Broker);

                await (tx.CommitAsync());
            }


            using (var s = OpenSession())
            using (var tx = s.BeginTransaction())
            {
                await (s.DeleteAsync("from System.Object"));

                await (tx.CommitAsync());
            }

            Assert.False(isInitialized);
        }
	}
}
