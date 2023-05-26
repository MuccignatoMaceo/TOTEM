using UnityEngine;

public class plateformeMobile : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private bool pingPongMode;
    [SerializeField] private Transform[] listeDestinations;
    private int sens = 1;
    private int indexDestination;

    void Start()
    {
        transform.position = listeDestinations[0].position;
    }

    void Update()
    {
        var destination = listeDestinations[indexDestination];
        transform.position = Vector2.MoveTowards(transform.position, destination.position, Time.deltaTime * speed);
        if (Vector2.Distance(transform.position,destination.position) < 0.01f)
        {
            indexDestination += sens;
            if (indexDestination >= listeDestinations.Length || indexDestination < 0)
            {
                if (pingPongMode)
                {
                    sens = -sens;
                    indexDestination += sens;
                }
                else
                {
                    indexDestination = 0;
                }
            }
        }
    }
}
