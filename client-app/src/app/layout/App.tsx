import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { Container } from "semantic-ui-react";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import Navbar from "../../features/nav/Navbar";
import api from "../api/api";
import { IActivity } from "../model/iActivity";
import { useAppSelector } from "../store/hooks";
import "./App.css";

function App() {
  const dispatch = useDispatch();
  const [postId, setPostID] = useState("");
  const { currentTab } = useAppSelector((state) => state.toggle);
  console.log(currentTab);

  const [activities, setActivities] = useState<IActivity[]>([]);
  const [selectedActivity, setSelectedActivity] = useState<IActivity | null>(
    null
  );
  const [editMode, setEditMode] = useState<boolean>(false);

  const handleSelectedActivity = (id: string) => {
    setSelectedActivity(activities.filter((x) => x.id === id)[0]);
    setEditMode(false);
  };
  const handleCreateActivity = (activity: IActivity) => {
    setActivities([...activities, activity]);
    setSelectedActivity(activity);
    setEditMode(false);
  };
  const handleEditActivity = (activity: IActivity) => {
    setActivities([
      ...activities.filter((x) => x.id !== activity.id),
      activity,
    ]);
    setSelectedActivity(activity);
    setEditMode(false);
  };

  const handleDeleteActivity = (id: string) => {
    setActivities([...activities.filter(x=>x.id!==id)]);
  };

  const handleOpenCreateForm = () => {
    setSelectedActivity(null);
    setEditMode(true);
  };
  useEffect(() => {
    api.Activities.list().then((res) => {
      let activities: IActivity[] = [];
      res.forEach((activity) => {
        activity.date = activity.date.split(".")[0];
        activities.push(activity);
      });
      setActivities(activities);
    });
  }, []);

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
        />
      </Container>
    </>
  );
}

export default App;
