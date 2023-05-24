using UnityEngine;


/// <summary>
/// Entidad básica. De esta heredan los personajes humanos, animales, monstruos, etc.
/// </summary>
public class Entity : MonoBehaviour {

    [SerializeField] private string entityName;
    [SerializeField] private string entityLastName;
    [SerializeField] private int id;

    [SerializeField] private int factionId;
    [SerializeField] private int bandId;

    [SerializeField] private bool isInAFaction;
    [SerializeField] private bool isInABand;

    [SerializeField] private Vector3 dir;

    private void Awake( ) {

        isInAFaction = false;
        isInABand = false;
        dir = Vector3.zero;

    }

    public bool IsInAFaction() {
    
        return isInAFaction;
    
    }

    /// <summary>
    /// Sets if its in a faction.
    /// </summary>
    /// <param name="fac"></param>
    public void SetInAFaction( bool fac ) {
    
        isInAFaction = fac;
    
    }

    public bool IsInABand() {

        return isInABand;
    
    }

    /// <summary>
    /// Sets if its in a band.
    /// </summary>
    /// <param name="fac"></param>
    public void SetInABand( bool fac ) {

        isInABand = fac;
    
    }

    public int GetId() {

        return id;

    }

    public string GetName( ) {

        return entityName;

    }

    public string GetLastName( ) {

        return entityLastName;

    }

    public void SetFactionId(int factionId ) {

        this.factionId = factionId;
    
    }

    public int GetFactionId( ) {

        return factionId;
    
    }

    public void SetBandId( int bandId ) {
    
        this.bandId = bandId;
    
    }

    public int GetBandId( ) {
    
        return bandId;
    
    }

    public void SetDirection( Vector3 dir ) {

        this.dir = dir;
    
    }

    public Vector3 GetDirection( ) {

        return dir;
    
    }


}
