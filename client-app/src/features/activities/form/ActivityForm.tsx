import React, { FormEvent, useState } from "react";
import { Button, Form, Segment } from "semantic-ui-react";
import { IActivity } from "../../../app/model/iActivity";
import { v4 as uuid } from "uuid";

interface IProps {
  setEditMode: (editMode: boolean) => void;
  activity: IActivity | null;
  createActivity: (activity: IActivity) => void;
  editActivity: (activity: IActivity) => void;
}
function ActivityForm({
  setEditMode,
  activity: initialFormState,
  createActivity,
  editActivity,
}: IProps) {
  const initializeForm = () => {
    if (initialFormState) return initialFormState;
    return {
      id: "",
      title: "",
      description: "",
      category: "",
      date: "",
      city: "",
      venue: "",
      isCancelled: true,
    };
  };

  const [activity, setActivity] = useState<IActivity>(initializeForm);
  const { title, description, category, date, city, venue } = activity;

  const handleOnChange = (
    event: FormEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = event.currentTarget;
    setActivity({ ...activity, [name]: value });
  };
  const handleSubmit = () => {
    if (activity.id.length === 0) {
      let newActivity = {
        ...activity,
        id: uuid(),
      };
      createActivity(newActivity);
    } else {
      editActivity(activity);
    }
  };
  return (
    <Segment clearing>
      <Form onSubmit={handleSubmit}>
        <Form.Input
          placeholder="Title"
          onChange={(e) => handleOnChange(e)}
          name="title"
          value={title}
        />
        <Form.TextArea
          placeholder="Description"
          onChange={(e) => handleOnChange(e)}
          name="description"
          value={description}
        />
        <Form.Input
          placeholder="Category"
          onChange={(e) => handleOnChange(e)}
          name="category"
          value={category}
        />
        <Form.Input
          type="datetime-local"
          placeholder="Date"
          onChange={(e) => handleOnChange(e)}
          name="date"
          value={date}
        />
        <Form.Input
          placeholder="City"
          onChange={(e) => handleOnChange(e)}
          name="city"
          value={city}
        />
        <Form.Input
          placeholder="Venue"
          onChange={(e) => handleOnChange(e)}
          name="venue"
          value={venue}
        />
        <Button floated="right">Submit</Button>
        <Button floated="right" onClick={() => setEditMode(false)}>
          Cancel
        </Button>
      </Form>
    </Segment>
  );
}

export default ActivityForm;
