import { combineReducers } from "redux";
import activityReducer from "./activityReducer";
import { commentReducer } from "./commentReducer";
import toggleReducer from "./toggleReducer";

const rootReducers = combineReducers({
  activities: activityReducer,
  toggle: toggleReducer,
  comments: commentReducer,
});
export default rootReducers;

export type RootState = ReturnType<typeof rootReducers>;
