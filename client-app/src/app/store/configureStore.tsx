import { createStore, applyMiddleware, Action } from 'redux';
import thunk, { ThunkAction } from 'redux-thunk';
import rootReducers from '../reducer/rootReducer';

export const store = createStore(rootReducers, {}, applyMiddleware(thunk));

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;