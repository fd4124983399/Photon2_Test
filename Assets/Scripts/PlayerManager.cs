using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.ABCDE.MyApp
{
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        void Update()
        {
            if (photonView.IsMine)
            {
                ProcessInputs();
            }
        }

        void ProcessInputs()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left * 50 * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += Vector3.right * 50 * Time.deltaTime;
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // 為玩家本人的狀態, 將 IsFiring 的狀態更新給其他玩家
                stream.SendNext(transform.position);
            }
            else
            {
                // 非為玩家本人的狀態, 單純接收更新的資料
                transform.position = (Vector3)stream.ReceiveNext();
            }
        }
    }
}