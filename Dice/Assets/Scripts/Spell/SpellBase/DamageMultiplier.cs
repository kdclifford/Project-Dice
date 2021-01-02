using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageMultiplier
{
   public static float GetDamageAmount(EElementalyType agentsElement, EElementalyType projectileElement)
    {
        switch (agentsElement)
        {
            case EElementalyType.Fire:

                switch (projectileElement)
                {
                    case EElementalyType.Nature:
                        return 0.5f;
                    case EElementalyType.Fire:
                        return 0.5f;
                    case EElementalyType.Water:
                        return 2f;
                }
                return 1f;
            case EElementalyType.Water:
                switch (projectileElement)
                {
                    case EElementalyType.Fire:
                        return 0.5f;
                    case EElementalyType.Water:
                        return 0.5f;
                    case EElementalyType.Electricity:
                        return 2f;
                }
                return 1f;
            case EElementalyType.Electricity:
                switch (projectileElement)
                {
                    case EElementalyType.Water:
                        return 0.5f;
                    case EElementalyType.Electricity:
                        return 0.5f;
                    case EElementalyType.Wind:
                        return 2f;
                }
                return 1f;
            case EElementalyType.Wind:
          
                switch (projectileElement)
                {
                    case EElementalyType.Electricity:
                        return 0.5f;
                    case EElementalyType.Wind:
                        return 0.5f;
                    case EElementalyType.Nature:
                        return 2f;
                }
                return 1f;
            case EElementalyType.Nature:
                switch (projectileElement)
                {
                    case EElementalyType.Wind:
                        return 0.5f;
                    case EElementalyType.Nature:
                        return 0.5f;
                    case EElementalyType.Fire:
                        return 2f;
                }
                return 1f;
        }
        return 1;
    }
}
