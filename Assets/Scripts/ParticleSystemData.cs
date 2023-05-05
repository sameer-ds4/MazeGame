using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "ParticleSystemData", menuName = "ScriptableObjects/ParticleSystemData", order = 1)]
public class ParticleSystemData : ScriptableObject
{
    [Header("ParticleSystem FXs Data")]
    public ParticleSystem[] FX;

    public void PlayFXs(Vector3 pos, Vector3 posOffSet, int FxsId,Vector3 size,Transform prient)
    {
        ParticleSystem particlesCash = Instantiate(FX[FxsId], (pos + posOffSet), Quaternion.Euler(new Vector3(0, 0, 0)));
        particlesCash.transform.localScale = size;
        particlesCash.Play();
       // particlesCash.transform.parent = LevelManager.Instance.currenterLevelContainer.trash;
        particlesCash.transform.parent = prient;
    }
    public void PlayFXs(Vector3 pos, Vector3 posOffSet, int FxsId, Vector3 size)
    {
        ParticleSystem particlesCash = Instantiate(FX[FxsId], (pos + posOffSet), Quaternion.Euler(new Vector3(-90, 0, 0)));
        particlesCash.transform.localScale = size;
        particlesCash.Play();        
    }
    public void PlayFXs(Vector3 pos, Vector3 posOffSet, int FxsId, Vector3 size, float daly)
    {
        ParticleSystem particlesCash = Instantiate(FX[FxsId], (pos + posOffSet), Quaternion.Euler(new Vector3(-0, 0, 0)));
        particlesCash.transform.localScale = size;
        DOVirtual.DelayedCall(daly, () =>
        {
            particlesCash.Play();
        });
    }
    public void PlayFXs(Vector3 pos, Vector3 posOffSet, int FxsId, Vector3 size, Vector3 rot)
    {
        ParticleSystem particlesCash = Instantiate(FX[FxsId], (pos + posOffSet), Quaternion.Euler(rot));
        particlesCash.transform.localScale = size;
        particlesCash.Play();
    }

    public void PlayEndFXs(Vector3 pos, Vector3 posOffSet, int FxsId, Vector3 size, Vector3 rot)
    {
        ParticleSystem particlesCash = Instantiate(FX[FxsId], (pos + posOffSet), Quaternion.Euler(new Vector3(0, 0, 0)));
        particlesCash.transform.localScale = size;
        particlesCash.transform.Rotate(rot, Space.Self);
        particlesCash.Play();

       // particlesCash.transform.parent = LevelManager.Instance.currenterLevelContainer.trash;
    }

    //public string[] showPanelText;
    //public Color[] showPanelTextColor;
}
