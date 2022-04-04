import { combineReducers } from "redux";
import { commentReducer } from "./commentReducer";
import toggleReducer from "./toggleReducer";

const rootReducers = combineReducers({
  toggle: toggleReducer,
  comments: commentReducer

});
export default rootReducers;

export type RootState = ReturnType<typeof rootReducers>;