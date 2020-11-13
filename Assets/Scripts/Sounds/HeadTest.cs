using System.Collections;
using System.Collections.Generic;
using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class HeadTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestUpdateEarPositions_Default()
        {
            GameObject obj = new GameObject();

            float width = 0.15f; // 15 cm
            float height = 0.20f; // 20 cm

            obj.AddComponent<Camera>();
            obj.AddComponent<Head>();

            Head head = obj.GetComponent<Head>();
            head.SetHeight(height);
            head.SetWidth(width);

            head.SetLeftEarlobe(new Earlobe());
            head.SetRightEarlobe(new Earlobe());
            head.UpdateEarlobePositions();

            Vector3 expectedLeftEarPosition = new Vector3(-0.075f, 0, 0);
            Vector3 expectedRightEarPosition = new Vector3(0.075f, 0, 0);

            Assert.AreEqual(expectedLeftEarPosition, head.GetLeftEarlobe().GetPosition());
            Assert.AreEqual(expectedRightEarPosition, head.GetRightEarlobe().GetPosition());
        }

        [Test]
        public void TestUpdateEarPositions_Tilt45()
        {
            GameObject obj = new GameObject();

            float width = 0.15f; // 15 cm
            float height = 0.20f; // 20 cm

            obj.AddComponent<Camera>();
            obj.AddComponent<Head>();

            Head head = obj.GetComponent<Head>();
            head.SetHeight(height);
            head.SetWidth(width);

            obj.transform.Rotate(new Vector3(0, 0, 45), Space.Self);
            obj.transform.position = new Vector3(0, 1.7f, 0);

            head.SetLeftEarlobe(new Earlobe());
            head.SetRightEarlobe(new Earlobe());
            head.UpdateEarlobePositions();

            Vector3 expectedLeftEarPosition = new Vector3(-0.053f, 1.647f, 0);
            Vector3 expectedRightEarPosition = new Vector3(0.053f, 1.753f, 0);
            
            Vector3 leftPosition = head.GetLeftEarlobe().GetPosition();
            Vector3 rightPosition = head.GetRightEarlobe().GetPosition(); // returning a vector is returning a copy not a reference

            leftPosition.x = (float)Math.Round(leftPosition.x * 1000f) /1000f; // round off to 3 decimal places
            rightPosition.x = (float)Math.Round(rightPosition.x * 1000f) /1000f; // round off to 3 decimal places
            leftPosition.y = (float)Math.Round(leftPosition.y * 1000f) /1000f; // round off to 3 decimal places
            rightPosition.y = (float)Math.Round(rightPosition.y * 1000f) /1000f; // round off to 3 decimal places

            Assert.AreEqual(expectedLeftEarPosition, leftPosition);
            Assert.AreEqual(expectedRightEarPosition, rightPosition);
        }
    }
}
