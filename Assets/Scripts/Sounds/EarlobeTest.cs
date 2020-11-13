using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class EarlobeTest
    {
        // Test default value
        [Test]
        public void TestGetPosition_0()
        {
            
            Earlobe instance = new Earlobe();

            Vector3 expected = new Vector3(0,0,0);
            Vector3 actual = instance.GetPosition();
            
            Assert.AreEqual(expected, actual);
        }
        // Test default value
        [Test]
        public void TestSetPosition_1_1_1()
        {
            
            Earlobe instance = new Earlobe();

            Vector3 expected = new Vector3(1,1,1);
            instance.SetPosition(new Vector3(1,1,1));
            Vector3 actual = instance.GetPosition();
            
            Assert.AreEqual(expected, actual);
        }
    }
}
