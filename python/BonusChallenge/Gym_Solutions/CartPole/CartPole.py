import gym 
import random

env = gym.make('CartPole-v1') # , render_mode="human"
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

import numpy as np
from keras.models import Sequential as seq
from keras.layers import Dense, Flatten
from keras.optimizers import Adam

def build_model(states, actions):
    model = seq()
    model.add(Flatten(input_shape=(1,actions))) # 1,2 is the input_shape, because there are 2 actions
    model.add(Dense(24, activation='relu'))
    model.add(Dense(24, activation='relu'))
    model.add(Dense(actions, activation='linear'))
    return model

model = build_model(states, actions)

model.summary()

from rl.agents import DQNAgent
from rl.policy import BoltzmannQPolicy
from rl.memory import SequentialMemory

def build_agent(model, actions):
    policy = BoltzmannQPolicy()
    memory = SequentialMemory(limit=50000, window_length=1)
    dqn = DQNAgent(model=model, memory=memory, policy=policy, 
                  nb_actions=actions, nb_steps_warmup=10, target_model_update=1e-2)
    return dqn

dqn = build_agent(model, actions)
dqn.compile(Adam(lr=1e-3), metrics=['mae'])
dqn.fit(env, nb_steps=50000, visualize=False, verbose=1)

scores = dqn.test(env, nb_episodes=100, visualize=False)
print(np.mean(scores.history['episode_reward']))

_ = dqn.test(env, nb_episodes=15, visualize=True)