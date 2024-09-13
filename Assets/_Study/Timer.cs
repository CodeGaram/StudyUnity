using UnityEngine;

namespace CodeGaram.Study
{
    public class Timer : MonoBehaviour
    {
        private double timer = 0;

        private void Update()
        {
            timer += Time.deltaTime;
        }

        public void OutputTimer(string callObjectName)
        {
            CanvasLog.AddTextLine($"{callObjectName}: {timer}");
        }
    }

}
