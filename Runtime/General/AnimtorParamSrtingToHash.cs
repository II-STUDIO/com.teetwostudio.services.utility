using UnityEngine;

[System.Serializable]
public struct AnimtorParamSrtingToHash
{
    public string name;
    [Range(0.02f, 0.1f)]
    public float dampTime;

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