using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour

{
   
        /* public Transform driverSeat; // El asiento del conductor en el tren
        public float moveSpeed = 10f; // Velocidad del tren

        private bool isDriving = false;
        private Player currentDriver;

        private void Update()
        {
            if (isDriving && currentDriver != null)
            {
                Vector3 direction = currentDriver.inputs.GetMovementCameraDirection(); // Asumiendo que tienes un método para obtener la dirección
                ControlTrain(direction);
            }
        }

        public void EnterTrain(Player player)
        {
            if (player != null)
            {
                currentDriver = player;
                player.transform.SetParent(driverSeat); // Montar en el tren
                player.transform.localPosition = Vector3.zero; // Ajustar posición del personaje
                player.transform.localRotation = Quaternion.identity; // Ajustar rotación del personaje
                StartDriving();
            }
        }

        public void ExitTrain()
        {
            if (currentDriver != null)
            {
                currentDriver.transform.SetParent(null); // Desmontar del tren
                currentDriver = null;
                StopDriving();
            }
        }

        private void StartDriving()
        {
            isDriving = true;
        }

        private void StopDriving()
        {
            isDriving = false;
        }

        public void ControlTrain(Vector3 direction)
        {
            // Lógica para controlar el tren según la dirección
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
      */  

}
