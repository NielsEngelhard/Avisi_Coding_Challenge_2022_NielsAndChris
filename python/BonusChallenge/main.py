from mlagents_envs.environment import UnityEnvironment
from gym_unity.envs import UnityToGymWrapper


def main():
    #direct this environment to your unity environment found in the zip.
    unity_env = UnityEnvironment("../AvisiToTheStars")
    env = UnityToGymWrapper(unity_env, uint8_visual=True, flatten_branched=True, allow_multiple_obs=True)
    x = lambda a, b: env.render() and False
    print(x)


if __name__ == '__main__':
    main()