import gym 
import random
import time

env = gym.make('CartPole-v1')
states = env.observation_space.shape[0]
actions = env.action_space.n

episodes = 10
for episode in range(1, episodes+1):
    state = env.reset()
    done = False
    score = 0
    
    while not done:
        env.render()
        action = random.choice([0,1])
        #n_state, reward, done, info = env.step(action)
        step_result = env.step(action)
        n_state = step_result[0]
        reward = step_result[1]
        done = step_result[2]
        info = step_result[3]
        score+=reward
    print('Episode:{} Score:{}'.format(episode, score))