using System.Collections.Generic;
using Mix.Core.Attributes;
using NUnit.Framework;

namespace Mix.Core.Tests
{
    [TestFixture]
    public class AliasAttributeFixture
    {
        private class NoAlias
        {
        }

        [Alias("onealias")]
        private class OneAlias
        {
        }

        [Alias("aliasone,aliastwo")]
        private class TwoAliases
        {
        }

        [Alias(" ALiasONe ,  aliasTWO ")]
        private class BadlyFormattedAliases
        {
        }

        [Test]
        public void ClassNoAlias()
        {
            IList<string> aliases = AliasAttribute.GetAliasesFrom(new NoAlias());
            Assert.IsNotNull(aliases);
            Assert.AreEqual(0, aliases.Count);
        }

        [Test]
        public void ClassWithOneAlias()
        {
            IList<string> aliases = AliasAttribute.GetAliasesFrom(new OneAlias());
            Assert.IsNotNull(aliases);
            Assert.IsTrue(aliases.Count == 1);
            Assert.AreEqual("onealias", aliases[0]);
        }

        [Test]
        public void ClassWithTwoAliases()
        {
            IList<string> aliases = AliasAttribute.GetAliasesFrom(new TwoAliases());
            Assert.IsNotNull(aliases);
            Assert.IsTrue(aliases.Count == 2);
            Assert.AreEqual("aliasone", aliases[0]);
            Assert.AreEqual("aliastwo", aliases[1]);
        }

        [Test]
        public void ClassWithBadlyFormattedAliases()
        {
            IList<string> aliases = AliasAttribute.GetAliasesFrom(new BadlyFormattedAliases());
            Assert.IsNotNull(aliases);
            Assert.IsTrue(aliases.Count == 2);
            Assert.AreEqual("ALiasONe", aliases[0]);
            Assert.AreEqual("aliasTWO", aliases[1]);
        }
    }
}