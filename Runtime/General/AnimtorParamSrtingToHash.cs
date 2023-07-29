using UnityEngine;

[System.Serializable]
public struct AnimtorParamSrtingToHash
{
    public string name;

    private bool isConverted;

    private int id;

    public int ID
    {
        get
        {
            if(!isConverted)
            {
                id = Animator.StringToHash(name);
                isConverted = true;
            }

            return id;
        }
    }
}