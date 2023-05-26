using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeWithMe : MonoBehaviour
{
    private Dictionary<int,Transform> dicoParents = new Dictionary<int,Transform>();
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Rigidbody2D rb))
        {
            if (rb.bodyType == RigidbodyType2D.Dynamic)
            {
                // On doit mémoriser le parent d'origine AVANT de le changer !
                dicoParents.Add(collision.gameObject.GetInstanceID(), collision.transform.parent);

                // On dit à l'objet qui rentre en contact avec nous de devenir notre enfant
                collision.transform.SetParent(transform, true);
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Rigidbody2D rb))
        {
            if (rb.bodyType == RigidbodyType2D.Dynamic)
            {
                // On dit à l'objet qui sort du contact avec nous de ne plus être notre enfant
                collision.transform.SetParent(dicoParents[collision.gameObject.GetInstanceID()], true);

                // On enleve l'entrée de dico correspondant à l'objet qui nous quitte
                dicoParents.Remove(collision.gameObject.GetInstanceID());
            }
        }
    }
}
