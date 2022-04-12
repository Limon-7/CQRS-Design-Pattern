import React, { SyntheticEvent, useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { Container } from "semantic-ui-react";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import Navbar from "../../features/nav/Navbar";
import api from "../api/api";
import { IActivity } from "../model/iActivity";
import { getActivities } from "../reducer/activityReducer";
import { useAppSelector } from "../store/hooks";
import "./App.css";
import LoadingComponent from "./LoadingComponent";

function App() {
  const dispatch = useDispatch();
  const activityState = useAppSelector((state) => state.activities);
  console.log("activityState", activityState);


  const [activities, setActivities] = useState<IActivity[]>([]);
  const [selectedActivity, setSelectedActivity] = useState<IActivity | null>(
    null
  );
  const [editMode, setEditMode] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(true);
  const [submitting, setSubmitting] = useState<boolean>(false);
  const [target, setTarget] = useState<string>("");

  const handleSelectedActivity = (id: string) => {
    setSelectedActivity(activities.filter((x) => x.id === id)[0]);
    setEditMode(false);
  };
  const handleCreateActivity = async (activity: IActivity) => {
    try {
      setSubmitting(true);
      let response = await api.Activities.create(activity);
      setActivities([...activities, activity]);
      setSelectedActivity(activity);
      setEditMode(false);
    } catch (error) {
      console.log(error);
    }
    setSubmitting(false);
  };
  const handleEditActivity = async (activity: IActivity) => {
    try {
      setSubmitting(true);
      await api.Activities.update(activity);
      setActivities([
        ...activities.filter((x) => x.id !== activity.id),
        activity,
      ]);
      setSelectedActivity(activity);
      setEditMode(false);
    } catch (error) {
      console.log(error);
    }
    setSubmitting(false);
  };

  const handleDeleteActivity = async (
    event: SyntheticEvent<HTMLButtonElement>,
    id: string
  ) => {
    try {
      setSubmitting(true);
      setTarget(event.currentTarget.name);
      await api.Activities.delete(id);
      setActivities([...activities.filter((x) => x.id !== id)]);
    } catch (error) {}
    setSubmitting(false);
  };

  const handleOpenCreateForm = () => {
    setSelectedActivity(null);
    setEditMode(true);
  };
  useEffect(() => {
    dispatch(getActivities());

    api.Activities.list().then((res) => {
      let activities: IActivity[] = [];
      res.forEach((activity) => {
        activity.date = activity.date.split(".")[0];
        activities.push(activity);
      });
      setActivities(activities);
      setLoading(false);
    });
  }, []);

  if (loading) return <LoadingComponent />;
  return (
    <>
      <Navbar handleOpenCreateForm={handleOpenCreateForm} />
      <Container style={{ marginTop: "7em" }}>
        <ActivityDashboard
          activities={activities}
          selectActivity={handleSelectedActivity}
          selectedActivity={selectedActivity!}
          editMode={editMode}
          setEditMode={setEditMode}
          setSelectedActivity={setSelectedActivity}
          createActivity={handleCreateActivity}
          editActivity={handleEditActivity}
          deleteActivity={handleDeleteActivity}
          submitting={submitting}
          target={target}
        />
      </Container>
    </>
  );
}

export default App;
