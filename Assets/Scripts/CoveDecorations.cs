using UnityEngine;
using System.Collections;

public class CoveDecorations : MonoBehaviour
{
    public enum CoveDecorationType { TikiTorch, TikiStatue, CthulhuStatue, Stonehead, IslandBar, Surfboard };
    
    public GameObject tikiTorch;
    GameObject[] tikiTorchPieces;
    public int tikiTorchToCollect;

    public GameObject tikiStatue;
    GameObject[] tikiStatuePieces;
    public int tikiStatueToCollect;

    public GameObject cthulhuStatue;
    GameObject[] cthulhuStatuePieces;
    public int cthulhuStatueToCollect;

    public GameObject stonehead;
    GameObject[] stoneheadPieces;
    public int stoneheadToCollect;

    public GameObject islandBar;
    GameObject[] islandBarPieces;
    public int islandBarToCollect;

    public GameObject surfboard;
    GameObject[] surfboardPieces;
    public int surfboardToCollect;

    DecorationAlertManager decorationAlerts;

    // Use this for initialization
    void Start()
    {
        tikiTorchPieces = GameObject.FindGameObjectsWithTag("TikiTorch");
        for (int i = 0; i < tikiTorchPieces.Length; i++)
        {
            tikiTorchPieces[i].GetComponent<CoveDecorationPiece>().pieceNumber = i;
            tikiTorchPieces[i].GetComponent<CoveDecorationPiece>().UpdateSprite();
        }
        tikiTorchToCollect = tikiTorchPieces.Length;
        tikiTorch.SetActive(false);
        
        tikiStatuePieces = GameObject.FindGameObjectsWithTag("TikiStatue");
        for (int i = 0; i < tikiStatuePieces.Length; i++)
        {
            tikiStatuePieces[i].GetComponent<CoveDecorationPiece>().pieceNumber = i;
            tikiStatuePieces[i].GetComponent<CoveDecorationPiece>().UpdateSprite();
        }
        tikiStatueToCollect = tikiStatuePieces.Length;
        tikiStatue.SetActive(false);
        
        cthulhuStatuePieces = GameObject.FindGameObjectsWithTag("CthulhuStatue");
        for (int i = 0; i < cthulhuStatuePieces.Length; i++)
        {
            cthulhuStatuePieces[i].GetComponent<CoveDecorationPiece>().pieceNumber = i;
            cthulhuStatuePieces[i].GetComponent<CoveDecorationPiece>().UpdateSprite();
        }
        cthulhuStatueToCollect = cthulhuStatuePieces.Length;
        cthulhuStatue.SetActive(false);

        stoneheadPieces = GameObject.FindGameObjectsWithTag("Stonehead");
        for (int i = 0; i < stoneheadPieces.Length; i++)
        {
            stoneheadPieces[i].GetComponent<CoveDecorationPiece>().pieceNumber = i;
            stoneheadPieces[i].GetComponent<CoveDecorationPiece>().UpdateSprite();
        }
        stoneheadToCollect = stoneheadPieces.Length;
        stonehead.SetActive(false);

        islandBarPieces = GameObject.FindGameObjectsWithTag("IslandBar");
        for (int i = 0; i < islandBarPieces.Length; i++)
        {
            islandBarPieces[i].GetComponent<CoveDecorationPiece>().pieceNumber = i;
            islandBarPieces[i].GetComponent<CoveDecorationPiece>().UpdateSprite();
        }
        islandBarToCollect = islandBarPieces.Length;
        islandBar.SetActive(false);

        surfboardPieces = GameObject.FindGameObjectsWithTag("Surfboard");
        for (int i = 0; i < surfboardPieces.Length; i++)
        {
            surfboardPieces[i].GetComponent<CoveDecorationPiece>().pieceNumber = i;
            surfboardPieces[i].GetComponent<CoveDecorationPiece>().UpdateSprite();
        }
        surfboardToCollect = surfboardPieces.Length;
        surfboard.SetActive(false);

        decorationAlerts = GameObject.FindGameObjectWithTag("DecorationAlerts").GetComponent<DecorationAlertManager>();
    }

    public void CollectPiece(CoveDecorationType currDecorationType, GameObject currPickup)
    {
        switch(currDecorationType)
        {
            case CoveDecorationType.TikiTorch:
                {
                    if (tikiTorchToCollect > 0)
                    {
                        tikiTorchToCollect--;
                        GameObject.Destroy(currPickup);
                        decorationAlerts.ActivateMessage((int)CoveDecorationType.TikiTorch, tikiTorchToCollect);

                        if (tikiTorchToCollect == 0)
                        {
                            tikiTorch.SetActive(true);
                            PlayerPrefs.SetInt("TikiTorch", 1); 
                        }
                        //Delete the piece, etc.
                    }
                    break;
                }
            case CoveDecorationType.TikiStatue:
                {
                    if (tikiStatueToCollect > 0)
                    {
                        tikiStatueToCollect--;
                        GameObject.Destroy(currPickup);
                        decorationAlerts.ActivateMessage((int)CoveDecorationType.TikiStatue, tikiStatueToCollect);

                        if (tikiStatueToCollect == 0)
                        {
                            tikiStatue.SetActive(true);
                            PlayerPrefs.SetInt("TikiStatue", 1);
                        }
                    }
                    break;
                }
            case CoveDecorationType.CthulhuStatue:
                {
                    if (cthulhuStatueToCollect > 0)
                    {
                        cthulhuStatueToCollect--;
                        GameObject.Destroy(currPickup);
                        decorationAlerts.ActivateMessage((int)CoveDecorationType.CthulhuStatue, cthulhuStatueToCollect);

                        if (cthulhuStatueToCollect == 0)
                        {
                            cthulhuStatue.SetActive(true);
                            PlayerPrefs.SetInt("Cthulhu", 1); 
                        }
                    }
                    break;
                }
            case CoveDecorationType.Stonehead:
                {
                    if(stoneheadToCollect > 0)
                    {
                        stoneheadToCollect--;
                        GameObject.Destroy(currPickup);
                        decorationAlerts.ActivateMessage((int)CoveDecorationType.Stonehead, stoneheadToCollect);

                        if (stoneheadToCollect == 0)
                        {
                            stonehead.SetActive(true);
                            PlayerPrefs.SetInt("Stonehead", 1);
                        }
                    }
                    break;
                }
            case CoveDecorationType.IslandBar:
                {
                    if (islandBarToCollect > 0)
                    {
                        islandBarToCollect--;
                        GameObject.Destroy(currPickup);
                        decorationAlerts.ActivateMessage((int)CoveDecorationType.IslandBar, islandBarToCollect);

                        if (islandBarToCollect == 0)
                        {
                            islandBar.SetActive(true);
                            PlayerPrefs.SetInt("IslandBar", 1);
                        }
                    }
                    break;
                }
            case CoveDecorationType.Surfboard:
                {
                    if (surfboardToCollect > 0)
                    {
                        surfboardToCollect--;
                        GameObject.Destroy(currPickup);
                        decorationAlerts.ActivateMessage((int)CoveDecorationType.Surfboard, surfboardToCollect);

                        if (surfboardToCollect == 0)
                        {
                            surfboard.SetActive(true);
                            PlayerPrefs.SetInt("Surfboard", 1); 
                        }
                    }
                    break;
                }
        }

    }
}
