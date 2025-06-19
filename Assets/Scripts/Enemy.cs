using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Skin enemySkin;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float minMoveSpeed = 2.5f;
    [SerializeField] private float maxMoveSpeed = 5.5f;
    [SerializeField] private ParticleSystem explosion;


    // [SerializeField] private 
    void Start()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * moveSpeed);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();

        if (other.name == "Ground")
        {
            player.Hurt();
        }
        else if (other.TryGetComponent(out player))
        {
            Skin.SkinColor playerSkinColor = player.playerSkin.GetSkinColor();
            Skin.SkinColor enemySkinColor = enemySkin.GetSkinColor();
            if (playerSkinColor != enemySkinColor)
            {
                player.Hurt();
            }
            else
            {
                player.IncreaseScore();
            }
        }
        Destroy(gameObject);

        SpawnExplosion();
    }

    private void SpawnExplosion()
    {
        SetExplosionColor();

        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    private void SetExplosionColor()
    {
        var main = explosion.main;
        switch (enemySkin.GetSkinColor())
        {
            case Skin.SkinColor.Red:
                main.startColor = new ParticleSystem.MinMaxGradient(Color.red, Color.white);
                break;
            case Skin.SkinColor.Green:
                main.startColor = new ParticleSystem.MinMaxGradient(Color.green, Color.white);
                break;
            case Skin.SkinColor.Blue:
                main.startColor = new ParticleSystem.MinMaxGradient(Color.cyan, Color.white);
                break;
            default:
                main.startColor = Color.white;
                break;
        }
    }
}
