using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fruitRigidBody;
    private Collider fruitCollider;

    private ParticleSystem juiceParticleEffect;

    private AudioSource audioSource;
    public AudioClip[] popSounds;

    public int points = 1;

    private void Awake()
    {
        fruitRigidBody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceParticleEffect = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        // Incrementa el puntaje
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        gameManager.IncreaseScore(points);

        // Play the pop sound
        if (audioSource != null)
        {
            PlayRandomPopSound(1f);
        }

        // Reproduce las partículas de jugo
        if (juiceParticleEffect != null)
        {
            juiceParticleEffect.Play();
        }

        // Desactiva el modelo completo de la fruta
        whole.SetActive(false);

        // Desactiva las colisiones
        fruitCollider.enabled = false;

        // Espera un momento para que las partículas terminen de reproducirse
        Destroy(gameObject, 0.5f); // Destruye el objeto después de 0.5 segundos
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();

            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }

    private void PlayRandomPopSound(float volume)
    {
        if (audioSource != null && popSounds.Length > 0)
        {
            AudioClip randomPop = popSounds[Random.Range(0, popSounds.Length)];
            audioSource.PlayOneShot(randomPop, volume);
        }
    }
}
