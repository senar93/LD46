﻿namespace Deirin.EB {
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using Deirin.Utilities;

    public class CollisionChecker : BaseBehaviour {
        public List<CollisionData> collisions;

        private void OnTriggerEnter ( Collider other ) {
            for ( int i = 0; i < collisions.Count; i++ ) {
                CollisionData c = collisions[i];
                if ( c.collisionType == CollisionData.CollisionType.trigger && c.eventType == CollisionData.EventType.enter ) {
                    switch ( c.searchFor ) {
                        case CollisionData.TargetType.Component:
                        Component comp = other.GetComponent(c.component.GetType());
                        if ( comp != null ) {
                            c.onComponentCollisionEnter.Invoke( comp );
                        }
                        break;
                        case CollisionData.TargetType.Layer:
                        if ( c.layer == ( c.layer | ( 1 << other.gameObject.layer ) ) ) {
                            c.onLayerCollisionEnter.Invoke();
                        }
                        break;
                        case CollisionData.TargetType.Tag:
                        string t = other.tag;
                        if ( t == c.tag ) {
                            c.onTagCollisionEnter.Invoke( t );
                        }
                        break;
                    }
                }
            }
        }

        private void OnTriggerStay ( Collider other ) {
            for ( int i = 0; i < collisions.Count; i++ ) {
                CollisionData c = collisions[i];
                if ( c.collisionType == CollisionData.CollisionType.trigger && c.eventType == CollisionData.EventType.stay ) {
                    switch ( c.searchFor ) {
                        case CollisionData.TargetType.Component:
                        Component comp = other.GetComponent(c.component.GetType());
                        if ( comp != null ) {
                            c.onComponentCollisionStay.Invoke( comp );
                        }
                        break;
                        case CollisionData.TargetType.Layer:
                        if ( c.layer == ( c.layer | ( 1 << other.gameObject.layer ) ) ) {
                            c.onLayerCollisionStay.Invoke();
                        }
                        break;
                        case CollisionData.TargetType.Tag:
                        string t = other.tag;
                        if ( t == c.tag ) {
                            c.onTagCollisionStay.Invoke( t );
                        }
                        break;
                    }
                }
            }
        }

        private void OnTriggerExit ( Collider other ) {
            for ( int i = 0; i < collisions.Count; i++ ) {
                CollisionData c = collisions[i];
                if ( c.collisionType == CollisionData.CollisionType.trigger && c.eventType == CollisionData.EventType.exit ) {
                    switch ( c.searchFor ) {
                        case CollisionData.TargetType.Component:
                        Component comp = other.GetComponent(c.component.GetType());
                        if ( comp != null ) {
                            c.onComponentCollisionExit.Invoke( comp );
                        }
                        break;
                        case CollisionData.TargetType.Layer:
                        if ( c.layer == ( c.layer | ( 1 << other.gameObject.layer ) ) ) {
                            c.onLayerCollisionExit.Invoke();
                        }
                        break;
                        case CollisionData.TargetType.Tag:
                        string t = other.tag;
                        if ( t == c.tag ) {
                            c.onTagCollisionExit.Invoke( t );
                        }
                        break;
                    }
                }
            }
        }

        private void OnCollisionEnter ( Collision collision ) {
            for ( int i = 0; i < collisions.Count; i++ ) {
                CollisionData c = collisions[i];
                if ( c.collisionType == CollisionData.CollisionType.collision && c.eventType == CollisionData.EventType.enter ) {
                    switch ( c.searchFor ) {
                        case CollisionData.TargetType.Component:
                        Component comp = collision.collider.GetComponent(c.component.GetType());
                        if ( comp != null ) {
                            c.onComponentCollisionEnter.Invoke( comp );
                        }
                        break;
                        case CollisionData.TargetType.Layer:
                        if ( c.layer == ( c.layer | ( 1 << collision.gameObject.layer ) ) ) {
                            c.onLayerCollisionEnter.Invoke();
                        }
                        break;
                        case CollisionData.TargetType.Tag:
                        string t = collision.collider.tag;
                        if ( t == c.tag ) {
                            c.onTagCollisionEnter.Invoke( t );
                        }
                        break;
                    }
                }
            }
        }

        private void OnCollisionStay ( Collision collision ) {
            for ( int i = 0; i < collisions.Count; i++ ) {
                CollisionData c = collisions[i];
                if ( c.collisionType == CollisionData.CollisionType.collision && c.eventType == CollisionData.EventType.stay ) {
                    switch ( c.searchFor ) {
                        case CollisionData.TargetType.Component:
                        Component comp = collision.collider.GetComponent(c.component.GetType());
                        if ( comp != null ) {
                            c.onComponentCollisionStay.Invoke( comp );
                        }
                        break;
                        case CollisionData.TargetType.Layer:
                        if ( c.layer == ( c.layer | ( 1 << collision.gameObject.layer ) ) ) {
                            c.onLayerCollisionStay.Invoke();
                        }
                        break;
                        case CollisionData.TargetType.Tag:
                        string t = collision.collider.tag;
                        if ( t == c.tag ) {
                            c.onTagCollisionStay.Invoke( t );
                        }
                        break;
                    }
                }
            }
        }

        private void OnCollisionExit ( Collision collision ) {
            for ( int i = 0; i < collisions.Count; i++ ) {
                CollisionData c = collisions[i];
                if ( c.collisionType == CollisionData.CollisionType.collision && c.eventType == CollisionData.EventType.exit ) {
                    switch ( c.searchFor ) {
                        case CollisionData.TargetType.Component:
                        Component comp = collision.collider.GetComponent(c.component.GetType());
                        if ( comp != null ) {
                            c.onComponentCollisionExit.Invoke( comp );
                        }
                        break;
                        case CollisionData.TargetType.Layer:
                        if ( c.layer == ( c.layer | ( 1 << collision.gameObject.layer ) ) ) {
                            c.onLayerCollisionExit.Invoke();
                        }
                        break;
                        case CollisionData.TargetType.Tag:
                        string t = collision.collider.tag;
                        if ( t == c.tag ) {
                            c.onTagCollisionExit.Invoke( t );
                        }
                        break;
                    }
                }
            }
        }

        public void RemoveCollision ( int _index ) {
            if ( collisions == null )
                collisions = new List<CollisionData>();

            collisions.RemoveAt( _index );
        }

        public void AddCollision () {
            if ( collisions == null )
                collisions = new List<CollisionData>();

            collisions.Add( new CollisionData() );
        }
    }

    [System.Serializable]
    public class CollisionData {
        public enum CollisionType { trigger = 0, collision = 1 }
        public enum TargetType { Component = 0, Layer = 1, Tag = 2 }
        public enum EventType { enter = 0, stay = 1, exit = 2 }

        public CollisionType collisionType = CollisionType.trigger;
        public EventType eventType = EventType.enter;
        public TargetType searchFor = TargetType.Layer;
        public LayerMask layer;
        public MonoBehaviour component;
        public string tag;
        public UnityEvent_String onTagCollisionEnter, onTagCollisionStay, onTagCollisionExit;
        public UnityEvent_Component onComponentCollisionEnter, onComponentCollisionStay, onComponentCollisionExit;
        public UnityEvent onLayerCollisionEnter, onLayerCollisionStay, onLayerCollisionExit;
    } 
}